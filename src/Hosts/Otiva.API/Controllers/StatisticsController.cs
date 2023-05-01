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
    public class StatisticsController : ControllerBase
    {
        public readonly IStatisticsAdsService _statisticsService;
        public StatisticsController(IStatisticsAdsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Получить статистику объявления
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("statisticsByAdId")]
        [ProducesResponseType(typeof(InfoAdStatisticsResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStatisticsBiAdIdId(Guid adId, CancellationToken cancellation)
        {
            var result = await _statisticsService.GetByAdIdAsync(adId, cancellation);
            return Ok(result);
        }

    }
}
