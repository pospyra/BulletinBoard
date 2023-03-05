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

        public async Task<Guid> CreateSubCategoryAsync(string name, Guid CategoryId)
        {
            var newSubcategory = new Domain.Subcategory()
            {
                Name = name,
                CategoryId = CategoryId
            };

            await _subcategoryRepository.Add(newSubcategory);
            return newSubcategory.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _subcategoryRepository.FindByIdAsync(id);
            if (category == null)
                throw new Exception("Подкатегории с таким идентификатором не сущесвует");

            await _subcategoryRepository.DeleteAsync(category);
        }

        public async Task<InfoSubcategory> EditSubCategoryAsync(Guid Id, string name, Guid CategoryId)
        {
            var existingCategory = await _subcategoryRepository.FindByIdAsync(Id);

            if (existingCategory == null)
                throw new Exception("Подкатегории с таким идентификатором не существует");

            existingCategory.Name = name;
            existingCategory.CategoryId = CategoryId;
            await _subcategoryRepository.EditSubcategoryAsync(existingCategory);

            return _mapper.Map<InfoSubcategory>(existingCategory);
        }

        public async Task<IReadOnlyCollection<InfoSubcategory>> GetAllAsync()
        {
            return await _subcategoryRepository.GetAll()
                .Select(a => new InfoSubcategory()
                {
                    Id = a.Id,
                    Name = a.Name             
                }).ToListAsync();
        }

        public async Task<InfoSubcategory> GetByIdAsync(Guid id)
        {
            var sub = await _subcategoryRepository.FindByIdAsync(id);
            if (sub == null) throw new Exception("Подкатегории с таким идентификатором не сущесвует");

            return _mapper.Map<InfoSubcategory>(sub);
        }
    }
}
