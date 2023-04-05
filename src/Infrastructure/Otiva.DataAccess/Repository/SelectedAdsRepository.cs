using Otiva.AppServeces.IRepository;
using Otiva.Domain;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.Repository
{
    public class SelectedAdsRepository : ISelectedAdsRepository
    {
        public readonly IBaseRepository<ItemSelectedAd> _baseRepository;

        public SelectedAdsRepository(IBaseRepository<ItemSelectedAd> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task Add(ItemSelectedAd model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(ItemSelectedAd model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(model);
        }

        public async Task<ItemSelectedAd> FindByIdAsync(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetByIdAsync(id);
        }

        public IQueryable<ItemSelectedAd> GetAll(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.GetAll();
        }
    }
}
