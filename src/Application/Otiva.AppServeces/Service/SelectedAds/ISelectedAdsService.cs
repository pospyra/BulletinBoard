using Otiva.Contracts.AdDto;
using Otiva.Contracts.SelectedAdDto;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.SelectedAds
{
    public interface ISelectedAdsService
    {
        /// <summary>
        /// Добавить объявление в избранное
        /// </summary>
        /// <param name="AdId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoSelectedResponse> AddSelectedAsync (Guid AdId, CancellationToken cancellation);

        /// <summary>
        /// Получить все избранные пользователя !!!! ТУДУ убрать парметр юзерайди
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoSelectedResponse>> GetSelectedUsersAsync(Guid UserId, int take, int skip);

        /// <summary>
        /// Удалить объявление из избранных
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid Id);
    }
}
