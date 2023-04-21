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
        /// <summary>
        /// Получить подкатегорию по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoSubcategory> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Добавить подкатегорию
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="CategoryId">Родительская категория</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateSubCategoryAsync(string name, Guid CategoryId, CancellationToken cancellation);

        /// <summary>
        /// Получить все подкатегории
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoSubcategory>> GetAllAsync(CancellationToken cancellation);

        /// <summary>
        /// Удалить подкатегорию 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Редактировать подкатегорию
        /// </summary>
        /// <param name="Id">ID</param>
        /// <param name="name">Название</param>
        /// <param name="CategoryId">Родительская категория</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoSubcategory> EditSubCategoryAsync(Guid Id, string name, Guid CategoryId, CancellationToken cancellation);
    }
}
