using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.Service.Category;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.AppServeces.Service.Photo;
using Otiva.AppServeces.Service.StatisticsAds;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.PhotoDto;
using Otiva.Domain.Ads;
using Otiva.Domain.Photos;

namespace Otiva.AppServeces.Service.Ad
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        private readonly IIdentityUserService _identityService;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly ILogger<AdService> _logger;
        private readonly IStatisticsAdsRepository _statisticsAdsRepository;
        public AdService(
            IStatisticsAdsRepository statisticsAdsRepository,
            IIdentityUserService identityService,
            IAdRepository adRepository, 
            IPhotoService photoService,
            IMapper mapper,
            ILogger<AdService> logger)
        {
            _photoService = photoService;
            _adRepository = adRepository;
            _mapper = mapper;
            _statisticsAdsRepository = statisticsAdsRepository;
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<Guid> CreateAdAsync(CreateAdRequest createAd, CancellationToken cancellation)
        {
            _logger.LogInformation("Создание объявления");
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            var newStatiscticsTableId = Guid.NewGuid();

            var staticsTable = new StatisticsTableAds
            {
                Id = newStatiscticsTableId,
            };
            await _statisticsAdsRepository.CreateStatistics(staticsTable);

            var newAd = _mapper.Map<Domain.Ads.Ad>(createAd);
            newAd.StatisticsTableAdsId = staticsTable.Id;
            newAd.DomainUserId = Guid.Parse(await _identityService.GetCurrentUserIdAsync(cancellation));

            await _adRepository.Add(newAd, cancellation);

            if (createAd.PhotoId != null)
            {
                foreach (var photoId in createAd.PhotoId)
                {
                    await _photoService.SetAdPhotoAsync(photoId, newAd.Id, cancellation);
                }
            }
            staticsTable.AdId = newAd.Id;
            await _statisticsAdsRepository.UpdateStatistics(staticsTable);
            return newAd.Id;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation("Удаление объявления");
            var existingAd = await _adRepository.FindByIdAsync(id, cancellation);
            if (existingAd == null)
                throw new InvalidOperationException("Объявления с таким идентификатором не сущесвует");

            var currentUser = await _identityService.GetCurrentUser(cancellation);

            if (existingAd.DomainUserId != Guid.Parse(currentUser.Id))
                throw new Exception("У вас не достаточно прав для работы с этим объвлением");

            await _adRepository.DeleteAsync(existingAd,cancellation);
        }

        public async Task<InfoAdResponse> EditAdAsync(Guid Id, UpdateAdRequest editAdRequest, CancellationToken cancellation)
        {
            _logger.LogInformation($"Редактирование объявления {Id}");

            var existingAd = await _adRepository.FindByIdAsync(Id, cancellation);
            if (existingAd == null)
                throw new Exception("Объявления с данным идентификатором не сущесвует");

            await _adRepository.EditAdAsync(_mapper.Map(editAdRequest, existingAd), cancellation);

            if (editAdRequest.PhotoId != null)
            {
                foreach (var photoId in editAdRequest.PhotoId)
                {
                    await _photoService.SetAdPhotoAsync(photoId, existingAd.Id, cancellation);
                }
            }
            //TODO удаление сущесвтующих фотографий, если их нет в дто 

            return _mapper.Map<InfoAdResponse>(existingAd);
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetByFilterAsync(SearchFilterAd search, SortAdsRequest sortArguments, CancellationToken cancellation)
        {
            _logger.LogInformation("Получение объявлений. Получение объявлений по заданному фильтру");

            var query = await _adRepository.GetByFilterAsync(search, cancellation);
            var res =  query.Select(p => new InfoAdResponse
            {
                Id = p.Id,
                Name = p.Name,
                DomainUserId = p.DomainUserId,
                SubcategoryId = p.SubcategoryId,
                Description = p.Description,
                CreateTime = p.CreateTime,
                Region = p.Region,
                Price = p.Price,
                QuantityView = p.StatisticsAds.QuantityView,
                Photos = p.Photos?.Select(a => new InfoPhotoResponse
                {
                    PhotoId = a.Id,
                }).ToList(),
            });

            if (sortArguments.ByCreatedDate)
               res = res.OrderByDescending(x => x.CreateTime);

            else if (sortArguments.ByDesPrice)
               res =  res.OrderByDescending(p => p.Price);

            else if (sortArguments.ByAscPrice) 
               res = res.OrderBy(p=>p.Price);

            else if (sortArguments.ByPopular)
                res = res.OrderBy(p => p.QuantityView);

            return res.Skip((search.PageNumber - 1) * search.PageSize).Take(search.PageSize).ToList(); 
        }

        public async  Task<InfoAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение объявления по идентификатору {id}");

            var existAd = _adRepository.GetAll(cancellation)
                .Include(x=>x.StatisticsAds).Include(a=>a.Photos)
                .Where(x => x.Id == id).FirstOrDefault();

            if (existAd == null)
                throw new Exception("Объявления с таким идентификатором не сущесвует");
            var currentUser = Guid.Parse(await _identityService.GetCurrentUserIdAsync(cancellation));

            if(existAd.DomainUserId != currentUser)
            {
                var statistics = existAd.StatisticsAds;
                statistics.QuantityView = statistics.QuantityView + 1;
                _statisticsAdsRepository.UpdateStatistics(statistics);
            }

            var infoAd  = _mapper.Map<InfoAdResponse>(existAd);
            infoAd.Photos = existAd.Photos?.Select(a => new Contracts.PhotoDto.InfoPhotoResponse
            {
                PhotoId = a.Id,
            }).ToList();
            infoAd.QuantityView = existAd.StatisticsAds.QuantityView;

            return infoAd;
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetMyAdsAsync(int pageNumber, int pageSize, CancellationToken cancellation)
        {
            _logger.LogInformation("Получение объявлений принадлежавших текущему пользователя");

            var currentUser = await _identityService.GetCurrentUserIdAsync(cancellation);

            var res = _adRepository.GetAll(cancellation)
                .Where(user => user.DomainUserId == Guid.Parse(currentUser))
                .Include(x => x.StatisticsAds).Include(a => a.Photos);

               return res.Select(p => new InfoAdResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    DomainUserId = p.DomainUserId,
                    SubcategoryId = p.SubcategoryId,
                    Description = p.Description,
                    Region = p.Region,
                    Price = p.Price,
                    QuantityView = p.StatisticsAds.QuantityView,
                    CreateTime = p.CreateTime,
                    Photos = p.Photos.Select(a => new Contracts.PhotoDto.InfoPhotoResponse
                    {
                        PhotoId = a.Id,
                    }).ToList(),
                }).OrderBy(d => d.CreateTime).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
