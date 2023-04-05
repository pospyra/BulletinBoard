using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.ReviewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Review
{
    public interface IReviewService
    {
        /// <summary>
        /// Получить отзыв по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InfoReviewResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Отправить отзыв на продавца
        /// </summary>
        /// <param name="createReview"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateReviewAsync(CreateReviewRequest createReview, CancellationToken cancellation);

        /// <summary>
        /// Получить все отзывы на продавца
        /// </summary>
        /// <param name="SellerId">Id продавца</param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoReviewResponse>> GetAllBySellerIdAsync(Guid SellerId, CancellationToken cancellation);

        /// <summary>
        /// Удалить отзыв
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Редактировать отзыв
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<InfoReviewResponse> EditReviewAsync(Guid id, string content, CancellationToken cancellation);
    }
}
