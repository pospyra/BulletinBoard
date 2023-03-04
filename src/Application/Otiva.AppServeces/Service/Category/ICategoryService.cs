using Otiva.Contracts.AdDto;
using Otiva.Contracts.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Category
{
    public interface ICategoryService
    {
        Task<InfoCategoryResponse> GetByIdAsync(Guid id);

        Task<Guid> CreateCategoryAsync(string name);

        Task<IReadOnlyCollection<InfoCategoryResponse>> GetAllAsync();

        Task DeleteAsync(Guid id);

        Task<InfoCategoryResponse> EditCategoryAsync(Guid Id,string name);

    }
}
