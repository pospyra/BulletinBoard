﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IIdentityUserService _identityService;
        private ILogger<AccountController> _logger;
        public AccountController(
            IIdentityUserService identityService,
            ILogger<AccountController> logger
            )
        {
            _identityService = identityService;
            _logger = logger;
        }

        /// <summary>
        /// Получить id текущего пользователя
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("currentUserId")]
        [ProducesResponseType(typeof(IReadOnlyCollection<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurrenUserId(CancellationToken cancellation)
        {
            var result = await _identityService.GetCurrentUserIdAsync(cancellation);
            return Ok(result);
        }

        /// <summary>
        /// Получить текущего IdentityUser
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
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
        [HttpGet("user/confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code, CancellationToken cancellation)
        {
            _logger.LogInformation($"Подтверждение почты пользователя {userId}");
            await _identityService.ConfirmEmail(userId, code, cancellation);
            return Ok();
        }

        /// <summary>
        /// Изменить почту
        /// </summary>
        /// <param name="newEmail">Новая почта</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("user/sendTokenToChangeEmail")]
        [Authorize]
        public async Task<IActionResult> SendTokenOnChangeEmaiAsync(string newEmail, CancellationToken cancellation)
        {
            _logger.LogInformation("Запрос на изменение почты");
            await _identityService.SendTokenOnChangeEmaiAsync(newEmail, cancellation);
            return Ok();
        }

        /// <summary>
        /// Подтверждение смены почты
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newEmail"></param>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("user/confirmChangeEmail")]
        public async Task<IActionResult> ConfirmChangeEmail(string userId, string newEmail, string token, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Подтверждение смены почты пользователя {userId}");
            await _identityService.ConfirmChangeEmail(userId, newEmail, token, cancellationToken);
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
