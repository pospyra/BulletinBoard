using Microsoft.AspNetCore.Mvc;
using Otiva.AppServeces.Service.Review;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.ReviewDto;
using System.Net;

namespace Otiva.API.Controllers
{
    [ApiController]
    public class ReviewController : ControllerBase
    {

        public readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("/reviewAboutSeller{SellerId}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoReviewResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(Guid SellerId)
        {
            var result = await _reviewService.GetAllBySellerIdAsync(SellerId);

            return Ok(result);
        }

        [HttpGet("/review/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoReviewResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _reviewService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost("review/create")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoReviewResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateReviewAsync(CreateReviewRequest createReview, CancellationToken cancellation)
        {
            var result = await _reviewService.CreateReviewAsync(createReview, cancellation);

            return Created("", result);
        }

        [HttpPut("/review/update/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoReviewResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditReviewAsync(Guid id, string content)
        {
            var res = await _reviewService.EditReviewAsync(id, content);

            return Ok(res);
        }

        [HttpDelete("/review/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _reviewService.DeleteAsync(id);

            return NoContent();
        }
    }
}
