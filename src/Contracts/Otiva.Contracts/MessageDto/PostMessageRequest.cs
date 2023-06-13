using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.MessageDto
{
    /// <summary>
    /// Запрос отправки сообщения
    /// </summary>
    public class PostMessageRequest
    {
        /// <summary>
        /// Идентификатор получаетелся
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        public string Content { get; set; }

        //Идентификатор отправителя(текущего пользователя) получаем в сервисе 
    }
}
