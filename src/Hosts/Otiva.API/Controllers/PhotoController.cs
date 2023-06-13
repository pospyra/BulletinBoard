using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.Photo;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.PhotoDto;
using System.Net;

namespace Otiva.API.Controllers
{
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        #region  фотографий объявлений 
        [AllowAnonymous]
        [HttpGet("photoAd")]
        [ProducesResponseType(typeof(IReadOnlyCollection<>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPhotoAdAsync(Guid AdId, CancellationToken cancellation)
        {
            var result = await _photoService.GetPhotoAdAsync(AdId, cancellation);
            return Ok(result);
        }

        /// <summary>
        /// Добавить фото объвления в бд
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("photoAd")]
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
        [HttpDelete("/photoAd/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletePhotoAsync(Guid id, CancellationToken cancellation)
        {
            await _photoService.DeletePhotoAdAsync(id, cancellation);

            return NoContent();
        }
        #endregion

        #region  фотографий пользователей 
        /// <summary>
        /// Добавить фото пользователя в бд
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("photoUser")]
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
        [HttpDelete("/photoUser/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletePhotoUserAsync(Guid id, CancellationToken cancellation)
        {
            await _photoService.DeletePhotoUserAsync(id, cancellation);

            return NoContent();
        }
        #endregion
    }
}
