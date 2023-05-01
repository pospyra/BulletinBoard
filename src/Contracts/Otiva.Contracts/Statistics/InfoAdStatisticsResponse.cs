using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.Statistics
{
    public class InfoAdStatisticsResponse
    {
        /// <summary>
        /// Идентификатор записи 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор объявления
        /// </summary>
        public Guid? AdId { get; set; }

        /// <summary>
        /// Количество просмотров 
        /// </summary>
        public int QuantityView { get; set; } = 0;

        /// <summary>
        /// Количество добавлений объявления в избранные
        /// </summary>
        public int QuantityAddToFavorites { get; set; } = 0;
    }
}
