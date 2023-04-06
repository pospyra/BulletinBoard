using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.SelectedAdDto
{
    public class InfoSelectedResponse
    {
        public Guid Id { get; set; }

        public Guid DomainUserId { get; set; }

        public Guid AdId { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
