using Otiva.Contracts.SubcategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.CategoryDto
{
    /// <summary>
    /// Тнформация о категории
    /// </summary>
    public class InfoCategoryResponse
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коллекция подкатегорий
        /// </summary>
        public ICollection<InfoSubcategory> Subcategories { get; set; }
    }
}
