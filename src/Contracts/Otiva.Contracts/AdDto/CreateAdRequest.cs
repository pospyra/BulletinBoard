using Otiva.Contracts.Attributs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.AdDto
{
    /// <summary>
    /// Создание объявления
    /// </summary>
    public class CreateAdRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        [CheckCurseWord]
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [CheckCurseWord]
        [MaxLength(500, ErrorMessage = "Вы превысили максимальную длину - 500 символов")]
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор подкатегории
        /// </summary>
        [Required]
        public Guid SubcategoryId { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Идентификатор фотографии
        /// </summary>
        public ICollection<Guid>? PhotoId { get; set; }
    }
}
