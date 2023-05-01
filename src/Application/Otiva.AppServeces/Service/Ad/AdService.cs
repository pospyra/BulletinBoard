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
using Otiva.Domain.Ads;

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
        private readonly IStatisticsAdsService _statisticsAdsService;
        public AdService(
            IAdRepository adRepository, 
            IPhotoService photoService,
            IStatisticsAdsRepository statisticsAdsRepository,
            IMapper mapper,
            IStatisticsAdsService statisticsAdsService,
            IIdentityUserService identityService,
            ILogger<AdService> logger)
        {
            _photoService = photoService;
            _adRepository = adRepository;
            _mapper = mapper;
            _statisticsAdsService = statisticsAdsService;
            _statisticsAdsRepository = statisticsAdsRepository;
            _identityService = identityService;
            _logger = logger;
        }

        //!Осторожно говнокод. Переписать!
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

            if (existingAd.DomainUserId != Guid.Parse(currentUser.Id)
                || currentUser.Role.Contains("User"))
                throw new Exception("У вас не достаточно прав для работы с этим объвлением");

            await _adRepository.DeleteAsync(existingAd,cancellation);
        }

        public async Task<InfoAdResponse> EditAdAsync(Guid Id, UpdateAdRequest editAdRequest, CancellationToken cancellation)
        {
            _logger.LogInformation("Редактирование объявления");

            var existingAd = await _adRepository.FindByIdAsync(Id, cancellation);
            if (existingAd == null)
                throw new Exception("Объявления с таким идентификатором не сущесвует");

            await _adRepository.EditAdAsync(_mapper.Map(editAdRequest, existingAd), cancellation);

            return _mapper.Map<InfoAdResponse>(existingAd);

        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            _logger.LogInformation("Получение всех объявлений. Этот метод тестировочный. Его надо убрать");
            var collectionAds = await _adRepository.GetAllAsync(cancellation);
            var res =  collectionAds.Select(x => new InfoAdResponse
            {
                Id = x.Id,
                Name = x.Name,
                CreateTime = x.CreateTime,
                Description = x.Description,
                Price = x.Price,
                Region = x.Region,
                SubcategoryId = x.SubcategoryId,
                UserId = x.DomainUserId,
                Photos = x.Photos?.Select(a => new Contracts.PhotoDto.InfoPhotoResponse
                {
                    PhotoId = a.Id,
                }).ToList(),
            }).OrderByDescending(a=>a.CreateTime).Skip(skip).Take(take);
            return res.ToList();
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetByFilterAsync(SearchFilterAd search, SortAdsRequest sortArguments, CancellationToken cancellation)
        {
            _logger.LogInformation("Получение объявлений. Получение объявлений по заданному фильтру");

            var query = await _adRepository.GetByFilterAsync(search, cancellation);
            var res = query.Select(p => new InfoAdResponse
            {
                Id = p.Id,
                Name = p.Name,
                UserId = p.DomainUserId,
                CategoryId = search.CategoryId,
                SubcategoryId = p.SubcategoryId,
                Description = p.Description,
                Region = p.Region,
                Price = p.Price,
                QuantityView = p.StatisticsAds.QuantityView,
                Photos = p.Photos?.Select(a => new Contracts.PhotoDto.InfoPhotoResponse
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

            return res.Skip(search.skip).Take(search.take).ToList(); 
        }

        public async  Task<InfoAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение объявления по идентификатору {id}");

            var exitAd = _adRepository.GetAll(cancellation).Include(x=>x.StatisticsAds).Where(x => x.Id == id).FirstOrDefault();

            if (exitAd == null)
                throw new Exception("Объявления с таким идентификатором не сущесвует");

            var statistics = exitAd.StatisticsAds;
            statistics.QuantityView = statistics.QuantityView +1;
            statistics.QuantityAddToFavorites = statistics.QuantityAddToFavorites +1 ;
            _statisticsAdsRepository.UpdateStatistics(statistics);

            return _mapper.Map<InfoAdResponse>(exitAd);
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetMyAdsAsync(int take, int skip, CancellationToken cancellation)
        {
            _logger.LogInformation("Получение объявлений принадлежавших текущему пользователя");

            var currentUser = await _identityService.GetCurrentUserIdAsync(cancellation);

            var res = await _adRepository.GetAllAsync(cancellation);
            return res
                .Where(user=> user.DomainUserId == Guid.Parse(currentUser))
                .Select(p => new InfoAdResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    UserId = p.DomainUserId,
                    SubcategoryId = p.SubcategoryId,
                    Description = p.Description,
                    Region = p.Region,
                    Price = p.Price,
                    CreateTime = p.CreateTime,
                    Photos = p.Photos?.Select(a => new Contracts.PhotoDto.InfoPhotoResponse
                    {
                        PhotoId = a.Id,
                    }).ToList(),
                }).OrderBy(d => d.CreateTime).Skip(skip).Take(take).ToList();
        }
    }
}
