using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.SelectedAdDto
{
    /// <summary>
    /// Избранное объявление
    /// </summary>
    public class InfoSelectedResponse
    {
        /// <summary>
        /// Идентификатор избранного объявления
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid DomainUserId { get; set; }

        /// <summary>
        /// Идентификатор объявления
        /// </summary>
        public Guid AdId { get; set; }

        /// <summary>
        /// Время добавления объявления в избранные
        /// </summary>
        public DateTime DateAdded { get; set; }
    }
}
