using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Найти категорию по ID
        /// </summary>
        /// <param name="id">ID категории</param>
        /// <returns></returns>
        Task<Category> FindByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns>Категории с их подкатегориями</returns>
        IQueryable<Category> GetAll( CancellationToken cancellation);

        /// <summary>
        /// Добавить категорию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Add(Category model, CancellationToken cancellation);

        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task DeleteAsync(Category model, CancellationToken cancellation);

        /// <summary>
        /// Редактировать название категории
        /// </summary>
        /// <param name="model">Модель категории</param>
        /// <returns></returns>
        Task EditAdAsync(Category model, CancellationToken cancellation);
    }
}
