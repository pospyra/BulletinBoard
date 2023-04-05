using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Otiva.AppServeces.IRepository;
using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.SubcategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Subcategory
{
    public class SubcategoryService : ISubcategoryService
    {
        public readonly ISubcategoryRepository _subcategoryRepository;
        public readonly IMapper _mapper;
        public SubcategoryService(ISubcategoryRepository subcategoryRepository, IMapper mapper)
        {
            _subcategoryRepository = subcategoryRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateSubCategoryAsync(string name, Guid CategoryId, CancellationToken cancellation)
        {
            var newSubcategory = new Domain.Subcategory()
            {
                Name = name,
                CategoryId = CategoryId
            };

            await _subcategoryRepository.Add(newSubcategory, cancellation);
            return newSubcategory.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var category = await _subcategoryRepository.FindByIdAsync(id, cancellation);
            if (category == null)
                throw new Exception("Подкатегории с таким идентификатором не сущесвует");

            await _subcategoryRepository.DeleteAsync(category, cancellation);
        }

        public async Task<InfoSubcategory> EditSubCategoryAsync(Guid Id, string name, Guid CategoryId, CancellationToken cancellation)
        {
            var existingCategory = await _subcategoryRepository.FindByIdAsync(Id, cancellation);

            if (existingCategory == null)
                throw new Exception("Подкатегории с таким идентификатором не существует");

            existingCategory.Name = name;
            existingCategory.CategoryId = CategoryId;
            await _subcategoryRepository.EditSubcategoryAsync(existingCategory, cancellation);

            return _mapper.Map<InfoSubcategory>(existingCategory);
        }

        public async Task<IReadOnlyCollection<InfoSubcategory>> GetAllAsync(CancellationToken cancellation)
        {
            return await _subcategoryRepository.GetAll(cancellation)
                .Select(a => new InfoSubcategory()
                {
                    Id = a.Id,
                    Name = a.Name             
                }).ToListAsync();
        }

        public async Task<InfoSubcategory> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var sub = await _subcategoryRepository.FindByIdAsync(id, cancellation);
            if (sub == null) throw new Exception("Подкатегории с таким идентификатором не сущесвует");

            return _mapper.Map<InfoSubcategory>(sub);
        }
    }
}
