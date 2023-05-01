using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        public async Task<IReadOnlyCollection<InfoAdStatisticsResponse>> GetAllAsync(CancellationToken cancellation)
        {
            var statistics = await _statisticsAdsRepository.GetAllAsync(cancellation);
            return statistics.Select(a=> new InfoAdStatisticsResponse
            {
                Id = a.Id,
                AdId = a.AdId,
                QuantityAddToFavorites = a.QuantityAddToFavorites,
                QuantityView = a.QuantityView,
            }).ToList();
        }

        public async Task<InfoAdStatisticsResponse> GetByAdIdAsync(Guid AdId, CancellationToken cancellation)
        {
            var cacheKey = "AdStatistics";
            var adStatistics = _memoryCache.Get<InfoAdStatisticsResponse>(cacheKey);

            if (adStatistics != null)
                return adStatistics;

            var statisticsToAd = await _statisticsAdsRepository.FindWhere(a => a.AdId == AdId);
            if (statisticsToAd == null)
                throw new ApplicationException("Статистика по данному объявлению не найдена ");

            var staticsDto = _mapper.Map<InfoAdStatisticsResponse>(statisticsToAd);
            _memoryCache.Set(cacheKey, staticsDto, TimeSpan.FromMinutes(2));
            return staticsDto;
        }
    }
}
