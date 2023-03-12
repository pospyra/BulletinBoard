using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.Photo;
using Otiva.Contracts.AdDto;
using System.Net;

namespace Otiva.API.Controllers
{
    [ApiController]
    public class PhotoController : ControllerBase
    {
        public readonly IPhotoService _photoService;
        public PhotoController(IPhotoService photoService) 
        {
            _photoService = photoService;
        }

        [HttpPost("photo/create")]
        [ProducesResponseType(typeof(IReadOnlyCollection<>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreatePhotoAsync(IFormFile file)
        {
            byte[] photo;
            await using (var ms = new MemoryStream())
            await using (var fs = file.OpenReadStream())
            {
                await fs.CopyToAsync(ms);
                photo = ms.ToArray();
            }

            var result = await _photoService.AddPhotoAsync(photo);

            return Created("", result);
        }

        [HttpDelete("/photo/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletePhotoAsync(Guid id)
        {
            await _photoService.DeleteAsync(id);

            return NoContent();
        }
    }
}
