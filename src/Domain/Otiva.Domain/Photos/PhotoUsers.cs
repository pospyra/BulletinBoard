using Otiva.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Domain.Photos
{
    public class PhotoUsers
    {
        public Guid Id { get; set; }

        public string KodBase64 { get; set; }

        public Guid? DomainUserId { get; set; }

        public DomainUser DomainUser { get; set; }
    }
}
