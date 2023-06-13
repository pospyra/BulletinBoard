using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.IdentityService;
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
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IIdentityUserService _identityService;
        public MessageController(IMessageService messageService, IUserService userService, IIdentityUserService identityService)
        {
            _messageService = messageService;
            _userService = userService;
            _identityService = identityService;
        }

        /// <summary>
        /// Получить все сообщения с выбранным пользователем
        /// </summary>
        /// <param name="user2_Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Authorize]
        [HttpGet("/chat/{user2_Id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoMessageResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessageFromChatAsync(Guid user2_Id, CancellationToken cancellation)
        {
            var user1_Id = Guid.Parse(await _identityService.GetCurrentUserIdAsync(cancellation));

            if (user1_Id == user2_Id)
                throw new Exception("Нельзя создать чат с самим собой");

            var result = await _messageService.GetMessagesFromChatAsync(user2_Id, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("chat/message")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoMessageResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> PostMessageAsync(PostMessageRequest message, CancellationToken cancellation)
        {
            var result = await _messageService.PostMessageAsync(message, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Редактировать сообщение
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("/chat/message/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoReviewResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditMessageAsync(Guid id, ContentMessage text, CancellationToken cancellation)
        {
            var res = await _messageService.EditMessageAsync(id, text, cancellation);

            return Ok(res);
        }

        /// <summary>
        /// Удалить сообщение
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("/chat/message/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellation)
        {
            await _messageService.DeleteMessageAsync(id, cancellation);

            return NoContent();
        }
    }
}
