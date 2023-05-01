using AutoMapper;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.SelectedAdDto;
using Otiva.Domain;
using Otiva.Domain.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.MapProfile
{
    public class AdMapProfile : Profile
    {
        public AdMapProfile() 
        {
            CreateMap<Ad, InfoAdResponse>().ReverseMap();
            CreateMap<Ad, CreateAdRequest>().ReverseMap();
            CreateMap<Ad, UpdateAdRequest>().ReverseMap();
            CreateMap<ItemSelectedAd, InfoSelectedResponse>().ReverseMap();
        }   
       
    }
}
