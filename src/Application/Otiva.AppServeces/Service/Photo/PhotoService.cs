using Otiva.AppServeces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Photo
{
    public class PhotoService : IPhotoService
    {
        public readonly IPhotoRepository _photoRepository;
        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<Guid> AddPhotoAsync(byte[] photo, CancellationToken cancellation)
        {
            if (photo.Length > 5242880)
                throw new Exception("Слишком большой размер фотографий");

            var newPhoto = new Domain.Photo()
            {
                KodBase64 = Convert.ToBase64String(photo, 0, photo.Length)
            };
            await _photoRepository.Add(newPhoto, cancellation);

            return newPhoto.Id;
        }

        public async Task DeleteAsync(Guid photoId, CancellationToken cancellation)
        {
            var photoDel = await _photoRepository.FindByIdAsync(photoId, cancellation);
            await _photoRepository.DeleteAsync(photoDel, cancellation);
        }

        public async Task SetAdPhotoAsync(Guid PhotoId, Guid AdId, CancellationToken cancellation)
        {
            var photo = await _photoRepository.FindByIdAsync(PhotoId, cancellation);
            photo.AdId = AdId;
            await _photoRepository.UpdatePhotoAsync(photo, cancellation);
        }
    }
}
