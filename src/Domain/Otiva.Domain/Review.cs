using Otiva.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Domain
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid SellerId { get; set; }

        public Guid CustomerId { get; set; }
        
        public string Content { get; set; }

        public DateTime CreatedReview { get; set; } = DateTime.UtcNow;

        public DomainUser DomainUser { get; set; }
    }
}
