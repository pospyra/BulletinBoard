using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.ReviewDto
{
    /// <summary>
    /// Отзыв на продавца
    /// </summary>
    public class InfoReviewResponse
    {
        /// <summary>
        /// Идентификатор отзыва
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентифкатор продавца
        /// </summary>
        public Guid SellerId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Время отправки отзыва
        /// </summary>
        public DateTime CreatedReview { get; set; }
    }
}
