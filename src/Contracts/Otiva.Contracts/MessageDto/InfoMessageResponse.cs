using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.MessageDto
{
    /// <summary>
    /// Информация о сообщении
    /// </summary>
    public class InfoMessageResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Текст 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Время отправки
        /// </summary>
        public DateTime SendingTime { get; set; }

        /// <summary>
        /// Прочитано ли сообщение
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Идентификтаор отправителя
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        public Guid ReceiverId { get; set; }

    }
}
