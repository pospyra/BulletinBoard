using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.Message;
using Otiva.Contracts.MessageDto;
using Otiva.Contracts.ReviewDto;
using System.Net;

namespace Otiva.API.Controllers
{
    public class MessageController : ControllerBase
    {
        public readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("/chat/message")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoMessageResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessageFromChatAsync(Guid user1_Id, Guid user2_Id)
        {
            if (user1_Id == user2_Id)
                throw new Exception("Нельзя создать чат с самим собой");
            
            var result = await _messageService.GetMessageFromChatAsync(user1_Id, user2_Id);

            return Ok(result);
        }


        [HttpPost("chat/postMessage")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoMessageResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> PostMessageAsync(PostMessageRequest message)
        {
            var result = await _messageService.PostMessageAsync(message);

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
