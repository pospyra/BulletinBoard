using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.AdDto
{
    /// <summary>
    /// Поиск объявления по фильтрам
    /// </summary>
    public class SearchFilterAd
    {
        /// <summary>
        /// Название
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Идентификатор автора объявления
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public Guid? CategoryId { get; set; } 

        /// <summary>
        /// Идентификатор подкатегории
        /// </summary>
        public Guid? SubcategoryId { get; set; }

        /// <summary>
        /// Минимальное ограничение по цене
        /// </summary>
        public decimal? PriceFrom { get; set; }

        /// <summary>
        /// Максимальное ограничение по цене
        /// </summary>
        public decimal? PriceTo { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Значение не может быть меньше 1")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Количество объявлений на одной странице
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Значение не может быть меньше 0")]
        public int PageSize { get; set; }

    }
}
