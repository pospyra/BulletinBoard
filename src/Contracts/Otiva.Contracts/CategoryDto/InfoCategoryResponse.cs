using Otiva.Contracts.SubcategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.CategoryDto
{
    public class InfoCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<InfoSubcategory> Subcategories { get; set; }
    }
}
