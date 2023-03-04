using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.User;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.UserDto;
using System.Net;

namespace Otiva.API.Controllers
{
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _userService.GetAllAsync(take, skip);

            return Ok(result);
        }


        [HttpGet("/user/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost("/registration")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Registration(RegistrationOrUpdateRequest registration)
        {
            var result = await _userService.RegistrationAsync(registration);

            return Created("", result);
        }

        [HttpPut("/user/update/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditUserAsync(Guid id, RegistrationOrUpdateRequest edit)
        {
            var res = await _userService.EditUserAsync(id, edit);

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
