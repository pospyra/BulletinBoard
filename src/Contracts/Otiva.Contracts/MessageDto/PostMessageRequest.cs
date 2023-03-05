using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.MessageDto
{
    public class PostMessageRequest
    {
        public Guid ReceiverId { get; set; }

        public Guid SenderId { get; set; }

        public string Content { get; set; }
    }
}
