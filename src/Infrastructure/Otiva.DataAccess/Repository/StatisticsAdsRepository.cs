using Microsoft.EntityFrameworkCore;
using Otiva.AppServeces.IRepository;
using Otiva.Contracts.Statistics;
using Otiva.Domain;
using Otiva.Domain.Ads;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.Repository
{
    public class StatisticsAdsRepository : IStatisticsAdsRepository
    {
        private IBaseRepository<StatisticsTableAds> _baseRepository;

        public StatisticsAdsRepository(
            IBaseRepository<StatisticsTableAds> baseRepository )
        {
            _baseRepository = baseRepository;
        }

        public Task CreateStatistics(StatisticsTableAds statistics)
        {
            return _baseRepository.AddAsync(statistics);
        }

        public IQueryable<StatisticsTableAds> GetAll(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.GetAll();
        }

        public async Task<IReadOnlyCollection<StatisticsTableAds>> GetAllAsync(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetAll()
                .ToListAsync();
        }

        public async Task<StatisticsTableAds> FindWhere(Expression<Func<StatisticsTableAds, bool>> predicate)
        {
            var data = _baseRepository.GetAllFiltered(predicate);

            return await data.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task UpdateStatistics(StatisticsTableAds statistics)
        {
            _baseRepository.UpdateAsync( statistics );
        }

    }
}
