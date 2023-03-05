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
        Task<InfoReviewResponse> GetByIdAsync(Guid id);

        Task<Guid> CreateReviewAsync(CreateReviewRequest createReview, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoReviewResponse>> GetAllBySellerIdAsync(Guid SellerId);

        Task DeleteAsync(Guid id);

        Task<InfoReviewResponse> EditReviewAsync(Guid id, string content);
    }
}
