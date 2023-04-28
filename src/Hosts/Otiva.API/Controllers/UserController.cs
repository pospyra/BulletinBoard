using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.AppServeces.Service.User;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.UserDto;
using Otiva.Domain;
using System.Net;

namespace Otiva.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получить всех пользователе  
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("user/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip, CancellationToken cancellation)
        {
            var result = await _userService.GetAllAsync(take, skip, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Получить пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/user/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellation)
        {
            var result = await _userService.GetByIdAsync(id, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Получить текущего пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/user/current")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurrentDomainUser( CancellationToken cancellation)
        {
            var result = await _userService.GetCurrentDomainUserAsync( cancellation);
            return Ok(result);
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="registration"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("/registration")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Registration([FromQuery]RegistrationRequest registration, CancellationToken cancellation)
        {

            var result = await _userService.RegistrationAsync(registration, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Редактировать данные пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="edit"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("/user/update/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditUserAsync(Guid id, UpdateUserRequest edit, CancellationToken cancellation)
        {
                var res = await _userService.EditUserAsync(id, edit, cancellation);
                return Ok(res);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("/user/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAdAsync(Guid id, CancellationToken cancellation)
        {
            await _userService.DeleteAsync(id, cancellation);

            return NoContent();
        }
    }
}
