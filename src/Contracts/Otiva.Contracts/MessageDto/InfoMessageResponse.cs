using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.MessageDto
{
    public class InfoMessageResponse
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime SendingTime { get; set; }

        public bool Read { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

    }
}
