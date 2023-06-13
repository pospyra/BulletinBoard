using Otiva.Contracts.PhotoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.AdDto
{
    /// <summary>
    /// Информация об объявлении
    /// </summary>
    public class InfoAdResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Идентификатор подкатегории
        /// </summary>
        public Guid SubcategoryId { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Дата создания объявления
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Автор объявления
        /// </summary>
        public Guid? DomainUserId { get; set; }

        /// <summary>
        /// Количество просмотров 
        /// </summary>
        public int? QuantityView { get; set; } 


        public ICollection<InfoPhotoResponse>? Photos { get; set; }
    }
}
