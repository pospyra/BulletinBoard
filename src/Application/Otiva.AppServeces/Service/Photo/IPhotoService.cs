using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Photo
{
    public interface IPhotoService
    {
        public Task DeleteAsync(Guid photoId);

        public Task<Guid> AddPhotoAsync(byte[] photo);

        public Task SetAdPhotoAsync(Guid PhotoId, Guid AdId);
    }
}
