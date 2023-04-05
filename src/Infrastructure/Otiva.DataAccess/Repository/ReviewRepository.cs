using Otiva.AppServeces.IRepository;
using Otiva.Domain;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        public readonly IBaseRepository<Review> _baseRepository;

        public ReviewRepository(IBaseRepository<Review> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task Add(Review model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Review model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(model);   
        }

        public async Task EditAdAsync(Review edit, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<Review> FindByIdAsync(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetByIdAsync(id);
        }

        public IQueryable<Review> GetAll(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.GetAll();
        }
    }
}
