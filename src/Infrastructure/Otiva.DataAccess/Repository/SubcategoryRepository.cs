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
    public class SubcategoryRepository : ISubcategoryRepository
    {
        public readonly IBaseRepository<Subcategory> _baseRepository;

        public SubcategoryRepository(IBaseRepository<Subcategory> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task Add(Subcategory model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Subcategory del, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(del);
        }

        public async Task EditSubcategoryAsync(Subcategory edit, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<Subcategory> FindByIdAsync(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetByIdAsync(id);
        }

        public IQueryable<Subcategory> GetAll(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.GetAll();
        }
    }
}

