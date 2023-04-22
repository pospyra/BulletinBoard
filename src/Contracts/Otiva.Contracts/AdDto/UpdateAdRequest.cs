using Otiva.Contracts.Attributs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.AdDto
{
    public class UpdateAdRequest
    {
        [CheckCurseWord]
        public string? Name { get; set; }

        [CheckCurseWord]
        public string? Description { get; set; }

        public Guid? SubcategoryId { get; set; }

        public decimal? Price { get; set; }

        public string? Region { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Guid>? PhotoId { get; set; }
    }
}
