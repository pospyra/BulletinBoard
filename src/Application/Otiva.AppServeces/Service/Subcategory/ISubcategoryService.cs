using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.SubcategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Subcategory
{
    public interface ISubcategoryService
    {
        Task<InfoSubcategory> GetByIdAsync(Guid id);

        Task<Guid> CreateSubCategoryAsync(string name, Guid CategoryId);

        Task<IReadOnlyCollection<InfoSubcategory>> GetAllAsync();

        Task DeleteAsync(Guid id);

        Task<InfoSubcategory> EditSubCategoryAsync(Guid Id, string name, Guid CategoryId);
    }
}
