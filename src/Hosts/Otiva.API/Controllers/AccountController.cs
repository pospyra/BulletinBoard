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
    public class AccountController : ControllerBase
    {
        public readonly IIdentityUserService _identityService;
        public AccountController(IIdentityUserService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Получить id текущего пользователя
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("currentUserId")]
        [ProducesResponseType(typeof(IReadOnlyCollection<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurrenUserId(CancellationToken cancellation)
        {
            var result = await _identityService.GetCurrentUserIdAsync(cancellation);
            return Ok(result);
        }

        [HttpGet("currentUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoIdentityUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurrenUser(CancellationToken cancellation)
        {
            var result = await _identityService.GetCurrentUser(cancellation);
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

        /// <summary>
        /// Изменить пароль
        /// </summary>
        /// <param name="changePass"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(ChangePassword changePass, CancellationToken cancellation)
        {
            await _identityService.ChangePasswordAsync(changePass, cancellation);
            return Ok();
        }

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
    }
}
