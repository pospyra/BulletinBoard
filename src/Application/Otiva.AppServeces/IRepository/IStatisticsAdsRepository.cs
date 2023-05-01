using Otiva.Contracts.Statistics;
using Otiva.Domain.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface IStatisticsAdsRepository
    {
        /// <summary>
        /// Обновить счетчик просмотров
        /// </summary>
        public Task UpdateStatistics(StatisticsTableAds statistics);

        public Task CreateStatistics(StatisticsTableAds statistics);

        /// <summary>
        /// Получит данные статистики по объявлениям
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public IQueryable<StatisticsTableAds> GetAll(CancellationToken cancellation);

    }
}
