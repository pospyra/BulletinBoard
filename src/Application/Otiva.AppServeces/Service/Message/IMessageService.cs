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
        /// <summary>
        /// Удалить сообщение
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteMessageAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellation"></param>
        /// <returns>Id сообщения</returns>
        public Task<Guid> PostMessageAsync(PostMessageRequest message, CancellationToken cancellation);

        /// <summary>
        /// Редактировать сообщение
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Task<InfoMessageResponse> EditMessageAsync(Guid id, TextMessageRequest text, CancellationToken cancellation);

        /// <summary>
        /// Получить все сообщения с пользователем user2
        /// </summary>
        /// <param name="user2_Id">Пользователь с которым общается текущий пользователь</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<IReadOnlyCollection<InfoMessageResponse>> GetMessageFromChatAsync(Guid user2_Id, CancellationToken cancellation);
    }
}
