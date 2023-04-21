using Microsoft.Extensions.Logging;
using Otiva.AppServeces.IRepository.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Photo
{
    //TODO Переписать! Код нарушает DRY
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoAdsRepository _photoAdsRepository;
        private readonly IPhotoUsersRepository _photoUsersRepository;
        private readonly ILogger<PhotoService> _logger; 

        public PhotoService(
            IPhotoAdsRepository photoAdsRepository, 
            IPhotoUsersRepository photoUsersRepository,
            ILogger<PhotoService> logger)
        {
            _photoAdsRepository = photoAdsRepository;
            _photoUsersRepository = photoUsersRepository;   
            _logger = logger;
        }

        public async Task<Guid> AddPhotoAdAsync(byte[] photo, CancellationToken cancellation)
        {
            _logger.LogInformation("Добавление фотографии объявления в бд");

            if (photo.Length > 5242880)
                throw new ArgumentException("Слишком большой размер фотографий");

            var newPhoto = new Domain.Photos.PhotoAds()
            {
                KodBase64 = Convert.ToBase64String(photo, 0, photo.Length)
            };
            await _photoAdsRepository.Add(newPhoto, cancellation);

            return newPhoto.Id;
        }

        public async Task DeletePhotoAdAsync(Guid photoId, CancellationToken cancellation)
        {
            _logger.LogInformation("Удаление фотографии объявления в бд");
            var photoDel = await _photoAdsRepository.FindByIdAsync(photoId, cancellation);
            if (photoDel == null)
                throw new InvalidOperationException("Фотография не найдена");

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
                throw new ArgumentException("Слишком большой размер фотографий");

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
