using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.AppServeces.Service.User;
using Otiva.Contracts.ReviewDto;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Review
{
    public class ReviewService : IReviewService
    {
        public readonly IReviewRepository _reviewRepository;
        public readonly IUserService _userService;
        public readonly IMapper _mapper;
        public readonly IIdentityUserService _identityService;
        public ReviewService(
            IReviewRepository reviewRepository, 
            IMapper mapper, 
            IUserService userService,
            IIdentityUserService identityService)
        {
            _reviewRepository = reviewRepository;
            _userService = userService;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<Guid> CreateReviewAsync(CreateReviewRequest createReview, CancellationToken cancellation)
        {
            var newReview = _mapper.Map<Domain.Review>(createReview);
            newReview.CustomerId = Guid.Parse(await _identityService.GetCurrentUserId(cancellation));

            await _reviewRepository.Add(newReview, cancellation);
            return newReview.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var reviewDel = await _reviewRepository.FindByIdAsync(id, cancellation);
            if (reviewDel == null)
                throw new Exception("Отзыва с таким идентификатором не существует");

            await _reviewRepository.DeleteAsync(reviewDel, cancellation);
        }

        public async Task<InfoReviewResponse> EditReviewAsync(Guid id, string content, CancellationToken cancellation)
        {
            var exisitingReview = await _reviewRepository.FindByIdAsync(id,cancellation);
            if (exisitingReview == null)
                throw new Exception("Отзыва с таким идентификатором не существует");

            exisitingReview.Content= content;
            await _reviewRepository.EditAdAsync(exisitingReview, cancellation);   

            return _mapper.Map<InfoReviewResponse>(exisitingReview);

        }

        public async Task<IReadOnlyCollection<InfoReviewResponse>> GetAllBySellerIdAsync(Guid SellerId, CancellationToken cancellation)
        {
            //var list = _reviewRepository.GetAll().Where(x => x.SellerId == SellerId);

            //return await list.Select(p => _mapper.Map<InfoReviewResponse>(p))
            //   .OrderByDescending(p => p.Id).ToListAsync();

            return await _reviewRepository.GetAll(cancellation).Where(x => x.SellerId == SellerId)
               .Select(a => new InfoReviewResponse
               {
                   Id = a.Id,
                   SellerId= a.SellerId,
                   CustomerId = a.CustomerId,
                   Content= a.Content,  
                   CreatedReview = a.CreatedReview
               }).ToListAsync();
        }

        public async Task<InfoReviewResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var review = await _reviewRepository.FindByIdAsync(id, cancellation);
            if (review == null)
                throw new Exception("Отзыва с таким айди не найден");

            return _mapper.Map<InfoReviewResponse>(review);
        }
    }
}
