using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Otiva.AppServeces.Service.Ad;
using Otiva.Contracts.AdDto;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Otiva.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с объявлениями
    /// </summary>
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;
        private readonly ILogger<AdController> _logger;

        public AdController(IAdService adService, ILogger<AdController> logger)
        {
            _adService = adService;
            _logger = logger;
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
        [HttpGet("/adsCurrentUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMyAdsAsync(int pageNumber, int pageSize, CancellationToken cancellation)
        {
            if (pageSize < 0 || pageNumber <= 0 || pageNumber == null)
                throw new Exception("Некорректные данные. Убедитесь, pageNumber не меньше 1 и не null, и pageSize не меньшее 0 ");

            var result = await _adService.GetMyAdsAsync(pageNumber, pageSize, cancellation);

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
        [HttpGet("/ads/filter")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFilter([FromQuery] SearchFilterAd query, [FromQuery] SortAdsRequest sortArguments, CancellationToken cancellation)
        {
            var result = await _adService.GetByFilterAsync(query, sortArguments, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Создать объвления
        /// </summary>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("ad")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAdAsync([FromBody] CreateAdRequest createAd, CancellationToken cancellation)
        {
            _logger.LogInformation($"{JsonConvert.SerializeObject(createAd)}");
            var result = await _adService.CreateAdAsync(createAd, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Редактировать объявление
        /// </summary>
        /// <param name="id"></param>
        /// <param name="edit"></param>
        /// <returns></returns>
        [HttpPut("/ad/{id}")]
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
        [HttpDelete("/ad/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление объявления {id}");
            await _adService.DeleteAsync(id, cancellation);

            return NoContent();
        }
    }
}
