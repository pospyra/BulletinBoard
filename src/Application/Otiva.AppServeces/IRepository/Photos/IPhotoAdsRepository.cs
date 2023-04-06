using Otiva.Domain.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository.Photos
{
    public interface IPhotoAdsRepository
    {
        Task<PhotoAds> FindByIdAsync(Guid id, CancellationToken cancellation);

        IQueryable<PhotoAds> GetAll(CancellationToken cancellation);

        Task Add(PhotoAds model, CancellationToken cancellation);

        Task DeleteAsync(PhotoAds photo, CancellationToken cancellation);

        Task UpdatePhotoAsync(PhotoAds photo, CancellationToken cancellation);
    }
}
