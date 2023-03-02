using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Domain
{
    public class SelectedAd
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid AdId { get; set; }
    }
}
