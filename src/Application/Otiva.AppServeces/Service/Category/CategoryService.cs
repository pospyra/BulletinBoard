using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Otiva.AppServeces.IRepository;
using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.SubcategoryDto;

namespace Otiva.AppServeces.Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(
            ICategoryRepository acategoryRepository,
            IMapper mapper,
            ISubcategoryRepository subcategoryRepository,
            IMemoryCache memoryCache,
            ILogger<CategoryService> logger)
        {
            _categoryRepository = acategoryRepository;
            _mapper = mapper;
            _subcategoryRepository = subcategoryRepository;
            _memoryCache = memoryCache;
            _logger = logger;
        }
        public async Task<Guid> CreateCategoryAsync(string name, CancellationToken cancellation)
        {
            _logger.LogInformation("Добавление новой категории");

            var newCategory = new Domain.Category()
            {
                Name = name,
            };

            await _categoryRepository.Add(newCategory, cancellation);
            return newCategory.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление категории с id {id}");

            var category = await _categoryRepository.FindByIdAsync(id, cancellation);
            if (category == null)
                throw new InvalidOperationException("Категории с таким идентификатором не существует");

            await _categoryRepository.DeleteAsync(category, cancellation);
        }

        public async Task<InfoCategoryResponse> EditCategoryAsync(Guid id, string name, CancellationToken cancellation)
        {
            _logger.LogInformation($"Редактирование категории с id {id}");

            var existingCategory = await _categoryRepository.FindByIdAsync(id, cancellation);
            if (existingCategory == null)
                throw new InvalidOperationException("Категории с таким идентификатором не сущесвует");

            existingCategory.Name = name;
            await _categoryRepository.EditAdAsync(existingCategory, cancellation);

            return _mapper.Map<InfoCategoryResponse>(existingCategory);
        }

        public async Task<IReadOnlyCollection<InfoCategoryResponse>> GetAllAsync(CancellationToken cancellation)
        {
            string ActiveCategoriesCachingKey = "ActiveCategories";
            if (_memoryCache.TryGetValue(ActiveCategoriesCachingKey, out IReadOnlyCollection<InfoCategoryResponse> result))
            {
                return result;
            }

            result = await _categoryRepository.GetAll(cancellation)
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

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };
            _memoryCache.Set(ActiveCategoriesCachingKey, result, options);

            return result;
        }

        public async Task<InfoCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id, cancellation);
            if (existingCategory == null)
                throw new Exception("Категории с таким идентификатором не сущесвует");

            existingCategory.Subcategories = await _subcategoryRepository.GetAll(cancellation).Where(c => c.CategoryId == id).ToListAsync();

            return new InfoCategoryResponse
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
