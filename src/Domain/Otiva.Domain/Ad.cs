using Otiva.Domain.Photos;
using Otiva.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Domain
{
    public class Ad
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? DomainUserId { get; set; }

        public Guid SubcategoryId { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public decimal Price { get; set; }

        public string Region { get; set; }

        public bool IsActive { get; set; } = true;

        public Subcategory Subcategory { get; set; }

        public DomainUser DomainUser { get; set; }

        public ICollection<PhotoAds> Photos { get; set; }
    }
}
