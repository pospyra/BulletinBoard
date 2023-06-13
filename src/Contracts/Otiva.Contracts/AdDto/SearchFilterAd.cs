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
        public string? Name { get; set; }

        public Guid? UserId { get; set; }

        public Guid? CategoryId { get; set; } 

        public Guid? SubcategoryId { get; set; }

        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

        public string? Region { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Значение не может быть меньше 1")]
        public int PageNumber { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Значение не может быть меньше 0")]
        public int PageSize { get; set; }

    }
}
