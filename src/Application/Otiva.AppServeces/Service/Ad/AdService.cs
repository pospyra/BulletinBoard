using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Ad
{
    public class AdService : IAdService
    {
        public readonly IAdRepository _adRepository;
        public readonly IUserService _userService;
        public readonly IIdentityUserService _identityService;
        public readonly IPhotoService _photoService;
        public readonly IMapper _mapper;
        public AdService(IAdRepository adRepository, IPhotoService photoService, IMapper mapper, IUserService userService, IIdentityUserService identityService)
        {
            _photoService = photoService;
            _adRepository = adRepository;
            _mapper = mapper;
            _userService = userService;
            _identityService = identityService;
        }

        public async Task<Guid> CreateAdAsync(CreateOrUpdateAdRequest createAd, CancellationToken cancellation)
        {
            try
            {
                if(cancellation.IsCancellationRequested)
                    throw new OperationCanceledException();

                var newAd = _mapper.Map<Domain.Ad>(createAd);
                newAd.DomainUserId = Guid.Parse(await _identityService.GetCurrentUserId(cancellation));

                await _adRepository.Add(newAd, cancellation);

                if(createAd.PhotoId != null)
                {
                    foreach (var photoId in createAd.PhotoId)
                    {
                        await _photoService.SetAdPhotoAsync(photoId, newAd.Id, cancellation);
                    }
                }

                return newAd.Id;
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var existingAd = await _adRepository.FindByIdAsync(id, cancellation);
            if (existingAd == null)
                throw new Exception("Объявления с таким идентификатором не сущесвует");

            var currentUser = await _identityService.GetCurrentUser(cancellation);

            if (existingAd.DomainUserId != currentUser.Id
                || currentUser.Role.Contains("User"))
                throw new Exception("У вас не достаточно прав для работы с этим объвлением");

            await _adRepository.DeleteAsync(existingAd,cancellation);
        }

        public async Task<InfoAdResponse> EditAdAsync(Guid Id, CreateOrUpdateAdRequest editAd, CancellationToken cancellation)
        {
            var existingAd = await _adRepository.FindByIdAsync(Id, cancellation);
            if (existingAd == null)
                throw new Exception("Объявления с таким идентификатором не сущесвует");

            await _adRepository.EditAdAsync(_mapper.Map(editAd, existingAd), cancellation);

            return _mapper.Map<InfoAdResponse>(editAd);

        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
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
                //Photos = x.Photos.Select(a => new Contracts.PhotoDto.InfoPhotoResponse
                //{
                //    KodBase64 = a.KodBase64,
                //}).ToList(),
            }).OrderByDescending(a=>a.CreateTime).Skip(skip).Take(take).ToList();
        }


        public async Task<IReadOnlyCollection<InfoAdResponse>> GetByFilterAsync(SearchFilterAd search, CancellationToken cancellation)
        {
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
                CreateTime = p.CreateTime
            }).OrderBy(x => x.CreateTime).Skip(search.skip).Take(search.take).ToList();
        }

        public async  Task<InfoAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var exitAd = await _adRepository.FindByIdAsync(id, cancellation);
            if (exitAd == null)
                throw new Exception("Объявления с таким идентификатором не сущесвует");
            return _mapper.Map<InfoAdResponse>(exitAd);
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetMyAdsAsync(int take, int skip, CancellationToken cancellation)
        {
            var currentUser = await _identityService.GetCurrentUserId(cancellation);

            var res = await _adRepository.GetAllAsync(cancellation);
            return res
                .Select(a => new InfoAdResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    SubcategoryId = a.SubcategoryId,
                    CreateTime = a.CreateTime,
                    UserId = a.DomainUserId,
                }).OrderBy(d => d.CreateTime).Skip(skip).Take(take).ToList();
        }
    }
}
