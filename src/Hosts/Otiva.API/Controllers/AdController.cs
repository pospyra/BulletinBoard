using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.Ad;
using Otiva.Contracts.AdDto;
using System.Net;

namespace Otiva.API.Controllers
{
    [ApiController]
    public class AdController : ControllerBase
    {
        public readonly IAdService _adService;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpGet("/ad/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(int take, int skip)
        {
            if (skip < 0 || take <= 0 || take == null)
                throw new Exception("Некорректные данные. Убедитесь, что skip >= 0, take > 0 и !null ");

            var result = await _adService.GetAllAsync(take, skip);

            return Ok(result);
        }


        [HttpGet("/ad/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _adService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("/ad/filter")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFilter([FromQuery] SearchFilterAd query)
        {
            if (query.skip < 0 || query.take <= 0 || query.take == null)
                throw new Exception("Некорректные данные. Убедитесь, что skip >= 0, take > 0 и !null ");

            var result = await _adService.GetByFilterAsync(query);

            return Ok(result);
        }

        [HttpPost("ad/createAd")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAdAsync(CreateOrUpdateAdRequest createAd)
        {
            var result = await _adService.CreateAdAsync(createAd);

            return Created("", result);
        }

        [HttpPut("/ad/update/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditAdAsync(Guid id, CreateOrUpdateAdRequest edit)
        {
            var res = await _adService.EditAdAsync(id, edit);

            return Ok(res);
        }

        [HttpDelete("/ad/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAdAsync(Guid id)
        {
            await _adService.DeleteAsync(id);

            return NoContent();
        }
    }
}
