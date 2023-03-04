using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Otiva.AppServeces.IRepository;
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
        public readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateReviewAsync(CreateReviewRequest createReview, Guid customerID)
        {
            var newReview = _mapper.Map<Domain.Review>(createReview);
            newReview.CreatedReview = DateTime.UtcNow;
            newReview.CustomerId = customerID;

            //TODO newReview.CustomerId = currentUSer;
            await _reviewRepository.Add(newReview);
            return newReview.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var reviewDel = await _reviewRepository.FindByIdAsync(id);
            if (reviewDel == null)
                throw new Exception("Отзыва с таким идентификатором не существует");

            await _reviewRepository.DeleteAsync(reviewDel);
        }

        public async Task<InfoReviewResponse> EditReviewAsync(Guid id, string content)
        {
            var exisitingReview = await _reviewRepository.FindByIdAsync(id);
            if (exisitingReview == null)
                throw new Exception("Отзыва с таким идентификатором не существует");

            exisitingReview.Content= content;
            await _reviewRepository.EditAdAsync(exisitingReview);

            return _mapper.Map<InfoReviewResponse>(exisitingReview);

        }

        public async Task<IReadOnlyCollection<InfoReviewResponse>> GetAllBySellerIdAsync(Guid SellerId)
        {
            //var list = _reviewRepository.GetAll().Where(x => x.SellerId == SellerId);

            //return await list.Select(p => _mapper.Map<InfoReviewResponse>(p))
            //   .OrderByDescending(p => p.Id).ToListAsync();

            return await _reviewRepository.GetAll().Where(x => x.SellerId == SellerId)
               .Select(a => new InfoReviewResponse
               {
                   Id = a.Id,
                   SellerId= a.SellerId,
                   CustomerId = a.CustomerId,
                   Content= a.Content,  
                   CreatedReview = a.CreatedReview
               }).ToListAsync();
        }

        public async Task<InfoReviewResponse> GetByIdAsync(Guid id)
        {
            var review = await _reviewRepository.FindByIdAsync(id);
            if (review == null)
                throw new Exception("Отзыва с таким айди не найден");

            return _mapper.Map<InfoReviewResponse>(review);
        }
    }
}
