using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.Message;
using Otiva.AppServeces.Service.User;
using Otiva.Contracts.MessageDto;
using Otiva.Contracts.ReviewDto;
using System.Net;

namespace Otiva.API.Controllers
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        public readonly IMessageService _messageService;
        public readonly IUserService _userService;

        public MessageController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        [HttpGet("/chat/message")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoMessageResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessageFromChatAsync(Guid user2_Id, CancellationToken cancellation)
        {
            var user1_Id = await _userService.GetCurrentUserId(cancellation);

            if (user1_Id == user2_Id)
                throw new Exception("Нельзя создать чат с самим собой");
            
            var result = await _messageService.GetMessageFromChatAsync( user2_Id, cancellation);

            return Ok(result);
        }


        [HttpPost("chat/postMessage")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoMessageResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> PostMessageAsync(PostMessageRequest message, CancellationToken cancellation)
        {
            var result = await _messageService.PostMessageAsync(message, cancellation);

            return Created("", result);
        }

        [HttpPut("/chat/message/update/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoReviewResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditMessageAsync(Guid id, TextMessageRequest text)
        {
            var res = await _messageService.EditMessageAsync(id, text);

            return Ok(res);
        }

        [HttpDelete("/chat/message/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _messageService.DeleteMessageAsync(id);

            return NoContent();
        }
    }
}
