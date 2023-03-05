using Otiva.Contracts.MessageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Message
{
    public interface IMessageService
    {
        public Task DeleteMessageAsync(Guid id);

        public Task<Guid> PostMessageAsync(PostMessageRequest message, CancellationToken cancellation);

        public Task<InfoMessageResponse> EditMessageAsync(Guid id, TextMessageRequest text);

        public Task<IReadOnlyCollection<InfoMessageResponse>> GetMessageFromChatAsync(Guid user2_Id, CancellationToken cancellation);
    }
}
