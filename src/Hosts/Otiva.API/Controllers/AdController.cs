using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Otiva.AppServeces.Service.Ad;
using Otiva.Contracts.AdDto;
using System.Net;

namespace Otiva.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с объявлениями
    /// </summary>
    [ApiController]
    public class AdController : ControllerBase
    {
        public readonly IAdService _adService;
        public readonly ILogger<AdController> _logger;

        public AdController(IAdService adService, ILogger<AdController> logger)
        {
            _adService = adService;
            _logger = logger;
        }

        /// <summary>
        /// Получить все объявления
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [AllowAnonymous]
        [HttpGet("/ad/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            if (skip < 0 || take <= 0 || take == null)
                throw new Exception("Некорректные данные. Убедитесь, что skip >= 0, take > 0 и !null ");

            var result = await _adService.GetAllAsync(take, skip, cancellation);


            return Ok(result);
        }

        /// <summary>
        /// Получить все объявления, которые опубликовал текущий пользователь
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Authorize]
        [HttpGet("/ad/getAdsCurrentUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMyAdsAsync(int take, int skip, CancellationToken cancellation)
        {
            if (skip < 0 || take <= 0 || take == null)
                throw new Exception("Некорректные данные. Убедитесь, что skip >= 0, take > 0 и !null ");

            var result = await _adService.GetMyAdsAsync(take, skip, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Получить объявление по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/ad/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var result = await _adService.GetByIdAsync(id, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Получить объявления по фильтрам
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [AllowAnonymous]
        [HttpGet("/ad/filter")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFilter([FromQuery] SearchFilterAd query, CancellationToken cancellation)
        {
            if (query.skip < 0 || query.take <= 0 || query.take == null)
                throw new Exception("Некорректные данные. Убедитесь, что skip >= 0, take > 0 и !null ");

            var result = await _adService.GetByFilterAsync(query, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Создать объвления
        /// </summary>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("ad/createAd")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAdAsync([FromQuery] CreateAdRequest createAd, CancellationToken cancellation)
        {
            var result = await _adService.CreateAdAsync(createAd, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Редактировать объявление
        /// </summary>
        /// <param name="id"></param>
        /// <param name="edit"></param>
        /// <returns></returns>
        [HttpPut("/ad/update/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditAdAsync(Guid id, UpdateAdRequest edit, CancellationToken cancellation)
        {
            var res = await _adService.EditAdAsync(id, edit, cancellation);

            return Ok(res);
        }

        /// <summary>
        /// Удалить объявление
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("/ad/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAdAsync(Guid id, CancellationToken cancellation)
        {
            await _adService.DeleteAsync(id, cancellation);

            return NoContent();
        }
    }
}
