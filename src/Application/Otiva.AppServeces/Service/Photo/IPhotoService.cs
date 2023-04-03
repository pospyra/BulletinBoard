using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Photo
{
    public interface IPhotoService
    {
        /// <summary>
        /// Удалить фотографию из бд
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        public Task DeleteAsync(Guid photoId);

        /// <summary>
        /// Добавить фотографию в бд
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public Task<Guid> AddPhotoAsync(byte[] photo);

        /// <summary>
        /// Добавить фотографию к объявлению
        /// </summary>
        /// <param name="PhotoId"></param>
        /// <param name="AdId"></param>
        /// <returns></returns>
        public Task SetAdPhotoAsync(Guid PhotoId, Guid AdId);
    }
}
