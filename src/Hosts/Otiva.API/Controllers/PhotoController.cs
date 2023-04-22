using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Добавить фото объвления в бд
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("photoAd/create")]
        [ProducesResponseType(typeof(IReadOnlyCollection<>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreatePhotoAsync(IFormFile file, CancellationToken cancellation)
        {
            byte[] photo;
            await using (var ms = new MemoryStream())
            await using (var fs = file.OpenReadStream())
            {
                await fs.CopyToAsync(ms);
                photo = ms.ToArray();
            }

            var result = await _photoService.UploadPhotoAdAsync(photo, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Удалить фото объявления из бд
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("/photoAd/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletePhotoAsync(Guid id, CancellationToken cancellation)
        {
            await _photoService.DeletePhotoAdAsync(id, cancellation);

            return NoContent();
        }



        /// <summary>
        /// Добавить фото пользователя в бд
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("photoUser/create")]
        [ProducesResponseType(typeof(IReadOnlyCollection<>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreatePhotoUserAsync(IFormFile file, CancellationToken cancellation)
        {
            byte[] photo;
            await using (var ms = new MemoryStream())
            await using (var fs = file.OpenReadStream())
            {
                await fs.CopyToAsync(ms);
                photo = ms.ToArray();
            }

            var result = await _photoService.UploadPhotoUserAsync(photo, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Удалить фото пользователя из бд
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("/photoUser/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletePhotoUserAsync(Guid id, CancellationToken cancellation)
        {
            await _photoService.DeletePhotoUserAsync(id, cancellation);

            return NoContent();
        }
    }
}
