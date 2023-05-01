using Otiva.Contracts.Statistics;
using Otiva.Domain.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="statistics"></param>
        /// <returns></returns>
        public Task CreateStatistics(StatisticsTableAds statistics);

        /// <summary>
        /// Найти по параметру
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<StatisticsTableAds> FindWhere(Expression<Func<StatisticsTableAds, bool>> predicate);

        /// <summary>
        /// Получить статистику по всем объявлниям
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<IReadOnlyCollection<StatisticsTableAds>> GetAllAsync(CancellationToken cancellation);

        public IQueryable<StatisticsTableAds> GetAll(CancellationToken cancellation);
    }
}
