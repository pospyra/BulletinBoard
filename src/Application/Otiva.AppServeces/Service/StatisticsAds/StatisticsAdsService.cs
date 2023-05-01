using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Otiva.AppServeces.IRepository;
using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.Statistics;
using Otiva.Contracts.SubcategoryDto;
using Otiva.Domain.Ads;

namespace Otiva.AppServeces.Service.StatisticsAds
{
    public class StatisticsAdsService : IStatisticsAdsService
    {
        private readonly IStatisticsAdsRepository _statisticsAdsRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<StatisticsAdsService> _logger;
        public StatisticsAdsService(
            IMapper mapper,
            IMemoryCache memoryCache,
            IStatisticsAdsRepository statisticsAdsRepository,
            ILogger<StatisticsAdsService> logger)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _logger = logger;
            _statisticsAdsRepository = statisticsAdsRepository;
        }

        public async Task<Guid> CreateStaticsTableAsync(Guid AdId, CancellationToken cancellation)
        {
            var newStatisticsTable = new StatisticsTableAds
            {
                AdId = AdId,
            };

            await _statisticsAdsRepository.CreateStatistics(newStatisticsTable);
            return newStatisticsTable.Id;
        }

        public Task<IReadOnlyCollection<InfoAdStatisticsResponse>> GetAllAsync(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
