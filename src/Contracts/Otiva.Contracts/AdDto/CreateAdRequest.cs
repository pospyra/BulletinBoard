using Otiva.Contracts.Attributs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.AdDto
{
    public class CreateAdRequest
    {
        [CheckCurseWord]
        public string Name { get; set; }

        [CheckCurseWord]
        [MaxLength(500, ErrorMessage = "Вы превысили максимальную длину - 500 символов")]
        public string Description { get; set; }

        [Required]
        public Guid SubcategoryId { get; set; }

        public decimal Price { get; set; }

        public string Region { get; set; }

        public ICollection<Guid>? PhotoId { get; set; }
    }
}
