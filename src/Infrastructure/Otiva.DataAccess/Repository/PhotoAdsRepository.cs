using Otiva.AppServeces.IRepository.Photos;
using Otiva.Domain.Photos;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.Repository
{
    public class PhotoAdsRepository : IPhotoAdsRepository
    {
        public readonly IBaseRepository<PhotoAds> _baseRepository;

        public PhotoAdsRepository(IBaseRepository<PhotoAds> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task Add(PhotoAds model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(PhotoAds photo, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(photo);
        }

        public async Task UpdatePhotoAsync(PhotoAds edit, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<PhotoAds> FindByIdAsync(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();
            return await _baseRepository.GetByIdAsync(id);
        }

        public IQueryable<PhotoAds> GetAll( CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.GetAll();
        }
    }
}
