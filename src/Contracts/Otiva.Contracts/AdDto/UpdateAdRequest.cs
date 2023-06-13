using Otiva.Contracts.Attributs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.AdDto
{
    /// <summary>
    /// Обновление фотографии
    /// </summary>
    public class UpdateAdRequest
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [CheckCurseWord]
        public string? Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [CheckCurseWord]
        public string? Description { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Идентификатор подкатегории
        /// </summary>
        public Guid? SubcategoryId { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// Актуальность объявления
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Коллекция идентификаторов фотографий объявления
        /// </summary>
        public ICollection<Guid>? PhotoId { get; set; }
    }
}
