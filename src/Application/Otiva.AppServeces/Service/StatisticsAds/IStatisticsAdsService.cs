using Otiva.Contracts.AdDto;
using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.StatisticsAds
{
    public interface IStatisticsAdsService
    {
        /// <summary>
        /// Получить статистику по всем объявлениям 
        /// </summary>
        /// <returns></returns
        Task<IReadOnlyCollection<InfoAdStatisticsResponse>> GetAllAsync(CancellationToken cancellation);

        /// <summary>
        /// Получить статистику по объявлению
        /// </summary>
        /// <returns></returns
        Task<InfoAdStatisticsResponse> GetByAdIdAsync(Guid AdId, CancellationToken cancellation);
    }
}
