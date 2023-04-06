using Otiva.AppServeces.IRepository.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Photo
{
    public class PhotoService : IPhotoService
    {
        public readonly IPhotoAdsRepository _photoAdsRepository;
        public readonly IPhotoUsersRepository _photoUsersRepository;
        public PhotoService(IPhotoAdsRepository photoAdsRepository, IPhotoUsersRepository photoUsersRepository)
        {
            _photoAdsRepository = photoAdsRepository;
            _photoUsersRepository = photoUsersRepository;   
        }

        public async Task<Guid> AddPhotoAdAsync(byte[] photo, CancellationToken cancellation)
        {
            if (photo.Length > 5242880)
                throw new Exception("Слишком большой размер фотографий");

            var newPhoto = new Domain.Photos.PhotoAds()
            {
                KodBase64 = Convert.ToBase64String(photo, 0, photo.Length)
            };
            await _photoAdsRepository.Add(newPhoto, cancellation);

            return newPhoto.Id;
        }

        public async Task DeletePhotoAdAsync(Guid photoId, CancellationToken cancellation)
        {
            var photoDel = await _photoAdsRepository.FindByIdAsync(photoId, cancellation);
            await _photoAdsRepository.DeleteAsync(photoDel, cancellation);
        }

        public async Task SetAdPhotoAsync(Guid PhotoId, Guid AdId, CancellationToken cancellation)
        {
            var photo = await _photoAdsRepository.FindByIdAsync(PhotoId, cancellation);
            photo.AdId = AdId;
            await _photoAdsRepository.UpdatePhotoAsync(photo, cancellation);
        }



        public async Task<Guid> AddPhotoUserAsync(byte[] photoUser, CancellationToken cancellation)
        {
            if (photoUser.Length > 5242880)
                throw new Exception("Слишком большой размер фотографий");

            var newPhoto = new Domain.Photos.PhotoUsers()
            {
                KodBase64 = Convert.ToBase64String(photoUser, 0, photoUser.Length)
            };
            await _photoUsersRepository.Add(newPhoto, cancellation);

            return newPhoto.Id;
        }

        public async Task DeletePhotoUserAsync(Guid photoUserId, CancellationToken cancellation)
        {
            var photoDel = await _photoUsersRepository.FindByIdAsync(photoUserId, cancellation);
            await _photoUsersRepository.DeleteAsync(photoDel, cancellation);
        }

        public async Task SetPhotoUserAsync(Guid PhotoId, Guid UserId, CancellationToken cancellation)
        {
            var photo = await _photoUsersRepository.FindByIdAsync(PhotoId, cancellation);
            photo.DomainUserId = UserId;
            await _photoUsersRepository.UpdatePhotoAsync(photo, cancellation);
        }
    }
}
