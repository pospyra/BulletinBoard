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
        public readonly ISelectedAdsService _selectedadService;

        public SelectedAdController(ISelectedAdsService selectedadService)
        {
            _selectedadService = selectedadService;
        }

        [HttpGet("/allSelectedByUserID{Id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoSelectedResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(Guid UserId, int take, int skip)
        {
            var result = await _selectedadService.GetSelectedUsersAsync(UserId, take, skip);

            return Ok(result);
        }


        [HttpPost("selectedAd/add")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoSelectedResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAdAsync(Guid AdId, CancellationToken cancellation)
        {
            var result = await _selectedadService.AddSelectedAsync(AdId, cancellation);

            return Created("", result);
        }


        [HttpDelete("/selected/delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAdAsync(Guid Id)
        {
            await _selectedadService.DeleteAsync(Id);

            return NoContent();
        }
    }
}
