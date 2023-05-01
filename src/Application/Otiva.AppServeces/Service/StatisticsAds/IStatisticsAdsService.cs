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
        /// Создать категорию
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Id созданной категории</returns>
        Task<Guid> CreateStaticsTableAsync(Guid AdId, CancellationToken cancellation);
        
        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns
        Task<IReadOnlyCollection<InfoAdStatisticsResponse>> GetAllAsync(CancellationToken cancellation);
    }
}
