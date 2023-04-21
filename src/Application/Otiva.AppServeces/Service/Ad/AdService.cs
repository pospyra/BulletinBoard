using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.AppServeces.Service.Photo;
using Otiva.AppServeces.Service.User;
using Otiva.Contracts.AdDto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Ad
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        private readonly IIdentityUserService _identityService;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly ILogger<AdService> _logger;
        public AdService(
            IAdRepository adRepository, 
            IPhotoService photoService, 
            IMapper mapper, 
            IIdentityUserService identityService,
            ILogger<AdService> logger)
        {
            _photoService = photoService;
            _adRepository = adRepository;
            _mapper = mapper;
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<Guid> CreateAdAsync(CreateOrUpdateAdRequest createAd, CancellationToken cancellation)
        {
            _logger.LogInformation("Создание объявления");
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            var newAd = _mapper.Map<Domain.Ad>(createAd);
            newAd.DomainUserId = Guid.Parse(await _identityService.GetCurrentUserId(cancellation));

            await _adRepository.Add(newAd, cancellation);

            if (createAd.PhotoId != null)
            {
                foreach (var photoId in createAd.PhotoId)
                {
                    await _photoService.SetAdPhotoAsync(photoId, newAd.Id, cancellation);
                }
            }

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

            if (existingAd.DomainUserId != currentUser.Id
                || currentUser.Role.Contains("User"))
                throw new Exception("У вас не достаточно прав для работы с этим объвлением");

            await _adRepository.DeleteAsync(existingAd,cancellation);
        }

        public async Task<InfoAdResponse> EditAdAsync(Guid Id, CreateOrUpdateAdRequest editAd, CancellationToken cancellation)
        {
            _logger.LogInformation("Редактирование объявления");

            var existingAd = await _adRepository.FindByIdAsync(Id, cancellation);
            if (existingAd == null)
                throw new Exception("Объявления с таким идентификатором не сущесвует");

            await _adRepository.EditAdAsync(_mapper.Map(editAd, existingAd), cancellation);

            return _mapper.Map<InfoAdResponse>(editAd);

        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            _logger.LogInformation("Получение всех объявлений. Этот метод тестировочный. Его надо убрать");
            var collectionAds = await _adRepository.GetAllAsync(cancellation);
            return collectionAds.Select(x => new InfoAdResponse
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
                    KodBase64 = a.KodBase64,
                }).ToList(),
            }).OrderByDescending(a=>a.CreateTime).Skip(skip).Take(take).ToList();
        }


        public async Task<IReadOnlyCollection<InfoAdResponse>> GetByFilterAsync(SearchFilterAd search, CancellationToken cancellation)
        {
            _logger.LogInformation("Получение объявлений. Получение объявлений по заданному фильтру");

            var query = await _adRepository.GetByFilterAsync(search, cancellation);

            return query.Select(p => new InfoAdResponse
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
                    KodBase64 = a.KodBase64,
                }).ToList(),
            }).OrderBy(x => x.CreateTime).Skip(search.skip).Take(search.take).ToList();
        }

        public async  Task<InfoAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение объявления по идентификатору {id}");

            var exitAd = await _adRepository.FindByIdAsync(id, cancellation);
            if (exitAd == null)
                throw new Exception("Объявления с таким идентификатором не сущесвует");
            return _mapper.Map<InfoAdResponse>(exitAd);
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetMyAdsAsync(int take, int skip, CancellationToken cancellation)
        {
            _logger.LogInformation("Получение объявлений принадлежавших текущему пользователя");

            var currentUser = await _identityService.GetCurrentUserId(cancellation);

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
                        KodBase64 = a.KodBase64,
                    }).ToList(),
                }).OrderBy(d => d.CreateTime).Skip(skip).Take(take).ToList();
        }
    }
}
