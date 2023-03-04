using AutoMapper;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.ReviewDto;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.MapProfile
{
    public class ReviewMapProfile : Profile
    {
        public ReviewMapProfile()
        {
            CreateMap<Review, InfoReviewResponse>().ReverseMap();
            CreateMap<Review, CreateReviewRequest>().ReverseMap();
        }

    }
}
