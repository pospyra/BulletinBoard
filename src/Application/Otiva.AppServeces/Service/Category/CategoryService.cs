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
        public async Task<Guid> CreateCategoryAsync(string name, CancellationToken cancellation)
        {
            try
            {
                var newCategory = new Domain.Category()
                {
                    Name = name,
                };

                await _categoryRepository.Add(newCategory, cancellation);
                return newCategory.Id;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var category = await _categoryRepository.FindByIdAsync(id, cancellation);
            if(category == null)
                throw new Exception("Категории с таким идентификатором не существует");

            await _categoryRepository.DeleteAsync(category, cancellation);
        }

        public async Task<InfoCategoryResponse> EditCategoryAsync(Guid Id, string name, CancellationToken cancellation)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(Id, cancellation);
            if (existingCategory == null)
                throw new Exception("Категории с таким идентификатором не сущесвует");

            existingCategory.Name = name;
            await _categoryRepository.EditAdAsync(existingCategory, cancellation);

            return _mapper.Map<InfoCategoryResponse>(existingCategory);
        }

        public async Task<IReadOnlyCollection<InfoCategoryResponse>> GetAllAsync(CancellationToken cancellation)
        {
            return await _categoryRepository.GetAll(cancellation)
                .Select(a => new InfoCategoryResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Subcategories = a.Subcategories.Select(c => new InfoSubcategory()
                    {
                        Id = c.Id,
                        Name = c.Name,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<InfoCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
           var existingCategory = await _categoryRepository.FindByIdAsync(id, cancellation);
            if (existingCategory == null)
                throw new Exception("Категории с таким идентификатором не сущесвует");

            existingCategory.Subcategories = await _subcategoryRepository.GetAll(cancellation).Where(c => c.CategoryId == id).ToListAsync();

            return  new InfoCategoryResponse
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                Subcategories = existingCategory.Subcategories.Select(c => new InfoSubcategory()
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToList()
            };

        }
    }
}
