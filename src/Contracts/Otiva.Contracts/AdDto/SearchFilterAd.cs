using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.AdDto
{
    public class SearchFilterAd
    {
        public string Name { get; set; }

        public Guid? UserId { get; set; }

        public Guid? SubcategoryId { get; set; }

        public DateTime CreateTime { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public int take { get; set; }

        public int skip { get; set; }

    }
}
