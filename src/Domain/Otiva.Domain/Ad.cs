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

        public Guid UserId { get; set; }

        public Guid SubcategoryId { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public decimal Price { get; set; }

        public string Region { get; set; }

        public Subcategory Subcategory { get; set; }

        public User User { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
