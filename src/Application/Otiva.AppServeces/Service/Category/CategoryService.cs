using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Otiva.AppServeces.IRepository;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.SubcategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Category
{
    public class CategoryService : ICategoryService
    {
        public readonly ICategoryRepository _categoryRepository;
        public readonly ISubcategoryRepository _subcategoryRepository;
        public readonly IMapper _mapper;
        public CategoryService(ICategoryRepository acategoryRepository, IMapper mapper, ISubcategoryRepository subcategoryRepository)
        {
            _categoryRepository = acategoryRepository;
            _mapper = mapper;
            _subcategoryRepository = subcategoryRepository;
        }
        public async Task<Guid> CreateCategoryAsync(string name)
        {
            var newCategory = new Domain.Category()
            {
                Name = name,
            };

            await _categoryRepository.AddAsync(newCategory);
            return newCategory.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _categoryRepository.FindById(id);
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<InfoCategoryResponse> EditCategoryAsync(Guid Id, string name)
        {
            var existingCategory = await _categoryRepository.FindById(Id);
            existingCategory.Name = name;
            await _categoryRepository.EditAdAsync(existingCategory);

            return _mapper.Map<InfoCategoryResponse>(existingCategory);
        }

        public async Task<IReadOnlyCollection<InfoCategoryResponse>> GetAll(int take, int skip)
        {
            return await _categoryRepository.GetAll()
                .Select(a => new InfoCategoryResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Subcategories = a.Subcategories.Select(c => new InfoSubcategory()
                    {
                        Id = c.Id,
                        Name = c.Name,
                    }).ToList()
                }).Skip(skip).Take(take).ToListAsync();

        }

        public async Task<InfoCategoryResponse> GetByIdAsync(Guid id)
        {
           var existingCategory = await _categoryRepository.FindById(id);
            var result = _mapper.Map<InfoCategoryResponse>(existingCategory);
            result.Subcategories = existingCategory.Subcategories.Select(c => new InfoSubcategory()
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
            return result;
        }
    }
}
