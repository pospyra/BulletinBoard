using Otiva.Contracts.AdDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Ad
{
    public interface IAdService
    {
        /// <summary>
        /// Получить объявление по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InfoAdResponse> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить объявления текущего пользователя
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoAdResponse>> GetMyAdsAsync(int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Создать объявление
        /// </summary>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateAdAsync(CreateOrUpdateAdRequest createAd, CancellationToken cancellation);

        /// <summary>
        /// Получить все объявления
        /// </summary>
        /// <param name="take">сколько объявлений взять</param>
        /// <param name="skip">сколько пропустить</param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoAdResponse>> GetAllAsync( int take, int skip);

        /// <summary>
        /// Удалить объявление
        /// </summary>
        /// <param name="id">Айди объявления</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Редактирвоать объвление
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="editAd"></param>
        /// <returns></returns>
        Task<InfoAdResponse> EditAdAsync(Guid Id, CreateOrUpdateAdRequest editAd);

        /// <summary>
        /// Получить объвления по фильтрам
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Коллекцию объвлений</returns>
        Task<IReadOnlyCollection<InfoAdResponse>> GetByFilterAsync(SearchFilterAd search);


    }
}
