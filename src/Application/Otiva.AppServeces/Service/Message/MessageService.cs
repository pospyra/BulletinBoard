using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public readonly IMessageRepository _messageRepository;
        public readonly IUserService _userService;
        public readonly IMapper _mapper;
        public readonly IIdentityUserService _identityService;

        public MessageService(IMessageRepository messageRepository, IMapper mapper, IUserService userService, IIdentityUserService identityService)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _userService = userService;
            _identityService = identityService;

        }
        public async Task DeleteMessageAsync(Guid id, CancellationToken cancellation)
        {
            var mesDel = await _messageRepository.FindByIdAsync(id, cancellation);
            if (mesDel == null)
                throw new Exception("Сообщения с таким айди не найдено");

            await _messageRepository.DeleteAsync(mesDel, cancellation);
        }

        public async Task<InfoMessageResponse> EditMessageAsync(Guid id, TextMessageRequest text, CancellationToken cancellation)
        {
            var existingMessage = await _messageRepository.FindByIdAsync(id, cancellation);

            if (existingMessage == null)
                throw new Exception("Сообщения с таким айди не найдено");

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
                }).ToListAsync();
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
