using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.AppServeces.Service.User;
using Otiva.Contracts.MessageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Message
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityUserService _identityService;
        private readonly ILogger<MessageService> _logger;

        public MessageService(
            IMessageRepository messageRepository, 
            IIdentityUserService identityService,
            ILogger<MessageService> logger,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _identityService = identityService;
            _logger = logger;
        }  

        public async Task DeleteMessageAsync(Guid id, CancellationToken cancellation)
        {
            var mesDel = await _messageRepository.FindByIdAsync(id, cancellation);
            if (mesDel == null)
                throw new InvalidOperationException("Сообщения с таким идентификатором не найдено");

            var existingUser = Guid.Parse(await _identityService.GetCurrentUserId(cancellation));

            if (mesDel.SenderId != existingUser)
            {
                _logger.LogWarning("Попытка удалить сообщение другого пользователя");
                throw new Exception("Пользователь не имеет права удалять не свои сообщения");
            }

            if (mesDel.SendingTime < DateTime.UtcNow.AddDays(-1))
                throw new Exception("Удалить сообщение можно было только в течении одного дня после отправки");

            await _messageRepository.DeleteAsync(mesDel, cancellation);
        }

        public async Task<InfoMessageResponse> EditMessageAsync(Guid id, TextMessageRequest text, CancellationToken cancellation)
        {
            var existingMessage = await _messageRepository.FindByIdAsync(id, cancellation);

            if (existingMessage == null)
                throw new InvalidOperationException("Сообщения с таким идентификатором не найдено");

            var existingUser = Guid.Parse(await _identityService.GetCurrentUserId(cancellation));

            if (existingMessage.SenderId != existingUser)
            {
                _logger.LogWarning("Попытка редактировать сообщение другого пользователя");
                throw new InvalidOperationException("Пользователь не имеет права редактировать не свои сообщения");
            } 

            if (existingMessage.SendingTime > DateTime.UtcNow.AddDays(-1))
                throw new ApplicationException("Редактировать сообщение можно было только в течении одного дня после отправки");

            existingMessage.Content = text.Text;
            await _messageRepository.EditAdAsync(existingMessage, cancellation);

            return _mapper.Map<InfoMessageResponse>(existingMessage);
        }

        public async Task<IReadOnlyCollection<InfoMessageResponse>> GetMessageFromChatAsync(Guid user2_Id, CancellationToken cancellation)
        {
            var user1_Id = Guid.Parse(await _identityService.GetCurrentUserId(cancellation));

            return await _messageRepository.GetAll(cancellation)
                .Where(x=>x.SenderId == user1_Id && x.ReceiverId == user2_Id 
                || x.SenderId == user2_Id && x.ReceiverId == user1_Id)
                .Select(a=> new InfoMessageResponse
                {
                    Id= a.Id,
                    SenderId = a.SenderId,  
                    ReceiverId = a.ReceiverId,
                    Content= a.Content,
                    SendingTime= a.SendingTime
                }).OrderByDescending(mes => mes.SendingTime)
                //TODO пагинация сообщений
                .ToListAsync();
        }

        public async Task<Guid> PostMessageAsync(PostMessageRequest message, CancellationToken cancellation)
        {
            var newMessage = _mapper.Map<Domain.Message>(message);
            newMessage.SenderId = Guid.Parse(await _identityService.GetCurrentUserId(cancellation));

            await _messageRepository.Add(newMessage, cancellation);
            return newMessage.Id;
        }
    }
}
