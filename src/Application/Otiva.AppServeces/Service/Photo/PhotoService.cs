﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Otiva.AppServeces.IRepository.Photos;
using Otiva.Contracts.PhotoDto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Photo
{
    //TODO вроде как нарушает dry, разделить на два сервиса
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

        #region  фотографий объявлений 
        public async Task<IReadOnlyCollection<PhotoContentResponse>> GetPhotoAdAsync(Guid AdId, CancellationToken cancellation)
        {
            _logger.LogInformation("Получение фотографии объявления");

            var photos = _photoAdsRepository.GetAll(cancellation)
                .Where(x=>x.AdId == AdId);

            return await photos.Select(a => new PhotoContentResponse
            {
                KodBase64 = a.KodBase64
            }).ToListAsync(); //TODO пагинация 
        }

        public async Task<Guid> UploadPhotoAdAsync(byte[] photo, CancellationToken cancellation)
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
        #endregion

        #region  фотографий пользователей 
        public async Task<Guid> UploadPhotoUserAsync(byte[] photoUser, CancellationToken cancellation)
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
        # endregion 
    }
}
