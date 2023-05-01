using Otiva.Contracts.AdDto;
using Otiva.Domain.Ads;
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
        Task<Ad> FindByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получить IReadOnlyCollection всех объявлений
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyCollection<Ad>> GetAllAsync( CancellationToken cancellation);


        /// <summary>
        /// Получить все объявления
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public IQueryable<Ad> GetAll(CancellationToken cancellation);

        /// <summary>
        /// Добавить объявление
        /// </summary>
        /// <param name="model">Модель объявления</param>
        /// <returns></returns>
        Task Add(Ad model, CancellationToken cancellation);

        /// <summary>
        /// Удалить объявление
        /// </summary>
        /// <param name="ad">Модель объявления</param>
        /// <returns></returns>
        Task DeleteAsync(Ad ad, CancellationToken cancellation);

        /// <summary>
        /// Редактировать объявление
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        Task EditAdAsync(Ad edit, CancellationToken cancellation);

        /// <summary>
        /// Получить объявление по заданному параметру
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<Ad> FindWhere(Expression<Func<Ad, bool>> predicate);

        /// <summary>
        /// Получить объявления по фильтру
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public Task<IReadOnlyCollection<Ad>> GetByFilterAsync(SearchFilterAd search, CancellationToken cancellation);

    }
}
