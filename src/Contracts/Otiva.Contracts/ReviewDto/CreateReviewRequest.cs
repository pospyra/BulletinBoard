using Otiva.Contracts.Attributs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.ReviewDto
{
    /// <summary>
    /// Отправка отзыва на продавца
    /// </summary>
    public class CreateReviewRequest
    {
        /// <summary>
        /// Идентификтаор продавца
        /// </summary>
        public Guid SellerId { get; set; }

        /// <summary>
        /// Контент
        /// </summary>
        [CheckCurseWord]
        public string Content { get; set; }
    }
}
