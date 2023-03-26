using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface IAdRepository
    {
        /// <summary>
        /// Найти объявление по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Ad> FindByIdAsync(Guid id);

        /// <summary>
        /// Получить все объявления
        /// </summary>
        /// <returns></returns>
        IQueryable<Ad> GetAll();

        /// <summary>
        /// Добавить объявление
        /// </summary>
        /// <param name="model">Модель объявления</param>
        /// <returns></returns>
        Task Add(Ad model);

        /// <summary>
        /// Удалить объявление
        /// </summary>
        /// <param name="ad">Модель объявления</param>
        /// <returns></returns>
        Task DeleteAsync(Ad ad);

        /// <summary>
        /// Редактировать объявление
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        Task EditAdAsync(Ad edit);
        public Task<Ad> FindWhere(Expression<Func<Ad, bool>> predicate);

    }
}
