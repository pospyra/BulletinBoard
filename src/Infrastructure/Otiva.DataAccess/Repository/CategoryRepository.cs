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
    public class CategoryRepository : ICategoryRepository
    {
        public readonly IBaseRepository<Category> _baseRepository;

        public CategoryRepository(IBaseRepository<Category> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task Add(Category model)
        {
            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Category del)
        {
            await _baseRepository.DeleteAsync(del);
        }

        public async Task EditAdAsync(Category edit)
        {
            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<Category> FindByIdAsync(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public IQueryable<Category> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
