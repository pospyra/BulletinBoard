using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.SubcategoryDto
{
    /// <summary>
    /// Подкатегория
    /// </summary>
    public class InfoSubcategory
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}
