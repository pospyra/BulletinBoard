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
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly IIdentityUserService _identityService;
        public UserController(IUserService userService, IIdentityUserService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }

        [HttpGet("user/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _userService.GetAllAsync(take, skip);

            return Ok(result);
        }

        [HttpGet("currentUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurrenUserI(CancellationToken cancellation)
        {
            //var result = await _userService.GetCurrentUserId(cancellation);
            return Ok();
        }

        [HttpGet("/user/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);

            return Ok(result);
        }

        //[HttpPost("addRole")]
        //[ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> AddUserToRole(Guid userId, string roleName)
        //{
        //    var res = await _userService.AddUserToRole(userId, roleName);

        //    return Ok(res);
        //}

        [HttpPost("login")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest userLogin)
        {
            var token = await _identityService.Login(userLogin);

            return Ok(new { Token = token, Message = "Success" });
        }

        [HttpPost("/registration")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Registration([FromBody]RegistrationOrUpdateRequest registration, IFormFile file)
        {
            byte[] photo = null;
            if (file != null)
            {
                await using (var ms = new MemoryStream())
                await using (var fs = file.OpenReadStream())
                {
                    await fs.CopyToAsync(ms);
                    photo = ms.ToArray();
                }
            }
            var result = await _userService.RegistrationAsync(registration, photo);

            return Created("", result);
        }

        [HttpPut("/user/update/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditUserAsync(Guid id, RegistrationOrUpdateRequest edit, IFormFile file)
        {
            byte[] photo;
            await using (var ms = new MemoryStream())
            await using (var fs = file.OpenReadStream())
            {
                await fs.CopyToAsync(ms);
                photo = ms.ToArray();
            }
            var res = await _userService.EditUserAsync(id, edit, photo);

            return Ok(res);
        }

        [HttpDelete("/user/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAdAsync(Guid id)
        {
            await _userService.DeleteAsync(id);

            return NoContent();
        }
    }
}
