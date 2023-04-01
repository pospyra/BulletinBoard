using Otiva.Contracts.PhotoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.AdDto
{
    public class InfoAdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Region { get; set; }

        public decimal? Price { get; set; }

        public DateTime CreateTime { get; set; }

        public Guid? UserId { get; set; }

        public Guid SubcategoryId { get; set; }

        public ICollection<InfoPhotoResponse> Photos { get; set; }
    }
}
