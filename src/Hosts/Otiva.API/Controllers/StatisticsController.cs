using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.AppServeces.Service.StatisticsAds;
using Otiva.AppServeces.Service.User;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.Statistics;
using Otiva.Contracts.UserDto;
using Otiva.Domain;
using System.Net;

namespace Otiva.API.Controllers
{
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsAdsService _statisticsService;
        public StatisticsController(IStatisticsAdsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Получить статистику объявления
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("statisticsAd{adId}")]
        [ProducesResponseType(typeof(InfoAdStatisticsResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStatisticsByAdIdId(Guid adId, CancellationToken cancellation)
        {
            var result = await _statisticsService.GetByAdIdAsync(adId, cancellation);
            return Ok(result);
        }

        [HttpGet("statisticsAds")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdStatisticsResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllStatisticsAd (CancellationToken cancellation)
        {
            var result = await _statisticsService.GetAllAsync(cancellation);
            return Ok(result);
        }
    }
}
