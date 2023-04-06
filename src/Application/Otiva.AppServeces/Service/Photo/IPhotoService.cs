﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Photo
{
    public interface IPhotoService
    {
        /// <summary>
        /// Добавить фото пользователя в бд
        /// </summary>
        /// <param name="photoUser"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<Guid> AddPhotoUserAsync(byte[] photoUser, CancellationToken cancellation);

        /// <summary>
        /// Удалить фото пользователя из бд
        /// </summary>
        /// <param name="photoUserId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task DeletePhotoUserAsync(Guid photoUserId, CancellationToken cancellation);

        /// <summary>
        /// Установит фото пользователя
        /// </summary>
        /// <param name="PhotoId"></param>
        /// <param name="UserId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task SetPhotoUserAsync(Guid PhotoId, Guid UserId, CancellationToken cancellation);

        /// <summary>
        /// Установит фото объявления
        /// </summary>
        /// <param name="PhotoId"></param>
        /// <param name="AdId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task SetAdPhotoAsync(Guid PhotoId, Guid AdId, CancellationToken cancellation);

        /// <summary>
        /// Удалить фото объвления из бд
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task DeletePhotoAdAsync(Guid photoId, CancellationToken cancellation);

        /// <summary>
        /// Добавит фото объявления в бд
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<Guid> AddPhotoAdAsync(byte[] photo, CancellationToken cancellation);
    }
}
