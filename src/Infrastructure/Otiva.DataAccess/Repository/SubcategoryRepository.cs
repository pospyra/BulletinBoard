using Otiva.Domain;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.Repository
{
    public class SubcategoryRepository
    {
        public readonly IBaseRepository<Subcategory> _baseRepository;

        public SubcategoryRepository(IBaseRepository<Subcategory> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task AddAsync(Subcategory model)
        {
            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Subcategory del)
        {
            await _baseRepository.DeleteAsync(del);
        }

        public async Task EditSubcategoryAsync(Subcategory edit)
        {
            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<Subcategory> FindById(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public IQueryable<Subcategory> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}

