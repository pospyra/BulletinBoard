using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.Ad;
using Otiva.AppServeces.Service.SelectedAds;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.SelectedAdDto;
using System.Net;
namespace Otiva.API.Controllers
{
    [ApiController]
    public class SelectedAdController : ControllerBase
    {
        private readonly ISelectedAdsService _selectedadService;

        public SelectedAdController(ISelectedAdsService selectedadService)
        {
            _selectedadService = selectedadService;
        }

        /// <summary>
        /// Получить избранные текущего пользоваеля
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("/allSelectedCurrentUser")]
        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoSelectedResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip, CancellationToken cancellation)
        {
            var result = await _selectedadService.GetSelectedUsersAsync(take, skip, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Добавить объявление в избранные
        /// </summary>
        /// <param name="AdId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("selectedAd/add")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoSelectedResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAdAsync(Guid AdId, CancellationToken cancellation)
        {
            var result = await _selectedadService.AddSelectedAsync(AdId, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Удалить объвление из избранных
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("/selected/delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAdAsync(Guid Id, CancellationToken cancellation)
        {
            await _selectedadService.DeleteAsync(Id, cancellation);

            return NoContent();
        }
    }
}
