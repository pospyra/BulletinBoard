using Otiva.AppServeces.IRepository;
using Otiva.Contracts.Statistics;
using Otiva.Domain;
using Otiva.Domain.Ads;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task UpdateStatistics(StatisticsTableAds statistics)
        {
            _baseRepository.UpdateAsync( statistics );
        }

    }
}
