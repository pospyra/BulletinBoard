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
        Task<InfoSubcategory> GetByIdAsync(Guid id, CancellationToken cancellation);

        Task<Guid> CreateSubCategoryAsync(string name, Guid CategoryId, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoSubcategory>> GetAllAsync(CancellationToken cancellation);

        Task DeleteAsync(Guid id, CancellationToken cancellation);

        Task<InfoSubcategory> EditSubCategoryAsync(Guid Id, string name, Guid CategoryId, CancellationToken cancellation);
    }
}
