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
    public class PhotoUsersRepository : IPhotoUsersRepository
    {
        public readonly IBaseRepository<PhotoUsers> _baseRepository;

        public PhotoUsersRepository(IBaseRepository<PhotoUsers> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task Add(PhotoUsers model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(PhotoUsers photo, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(photo);
        }

        public async Task UpdatePhotoAsync(PhotoUsers edit, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<PhotoUsers> FindByIdAsync(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();
            return await _baseRepository.GetByIdAsync(id);
        }

        public IQueryable<PhotoUsers> GetAll( CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.GetAll();
        }
    }
}
