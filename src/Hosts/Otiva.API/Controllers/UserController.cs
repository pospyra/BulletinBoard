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
        public readonly IIdentityUserService _identityService;
        public UserController(IUserService userService, IIdentityUserService identityService)
        {
            _userService = userService;
            _identityService = identityService;
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
        /// Получить текущего пользователя
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("currentUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurrenUserId(CancellationToken cancellation)
        {
            var result = await _identityService.GetCurrentUserId(cancellation);
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
        /// Подтвердить почту
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code">Код подтверждения</param>
        /// <returns></returns>
        [HttpGet("confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code, CancellationToken cancellation)
        {
            await _identityService.ConfirmEmail(userId, code, cancellation);
            return Ok();
        }

        //[HttpPost("addRole")]
        //[ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> AddUserToRole(Guid userId, string roleName)
        //{
        //    var res = await _userService.AddUserToRole(userId, roleName);

        //    return Ok(res);
        //}

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest userLogin, CancellationToken cancellation)
        {
            var token = await _identityService.Login(userLogin, cancellation);

            return Ok(new { Token = token, Message = "Success" });
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="registration"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("/registration")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Registration([FromQuery]RegistrationOrUpdateRequest registration, CancellationToken cancellation)
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
        public async Task<IActionResult> EditUserAsync(Guid id, RegistrationOrUpdateRequest edit, IFormFile file, CancellationToken cancellation)
        {
            byte[] photo;
            await using (var ms = new MemoryStream())
            await using (var fs = file.OpenReadStream())
            {
                await fs.CopyToAsync(ms);
                photo = ms.ToArray();

                var res = await _userService.EditUserAsync(id, edit, photo, cancellation);

                return Ok(res);
            }
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       // [Authorize]
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
