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
        /// <summary>
        /// Получить категорию по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InfoCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Создать категорию
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Id созданной категории</returns>
        Task<Guid> CreateCategoryAsync(string name, CancellationToken cancellation);
        
        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoCategoryResponse>> GetAllAsync(CancellationToken cancellation);

        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Htlfrnbhdjfnm rfntujhb.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<InfoCategoryResponse> EditCategoryAsync(Guid Id,string name, CancellationToken cancellation);

    }
}
