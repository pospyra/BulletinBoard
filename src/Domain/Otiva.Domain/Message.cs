using Otiva.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Domain
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime SendingTime { get; set; } = DateTime.UtcNow;

        public bool Read { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public DomainUser Sender { get; set; }
        public DomainUser Receiver { get; set; }
    }
}
