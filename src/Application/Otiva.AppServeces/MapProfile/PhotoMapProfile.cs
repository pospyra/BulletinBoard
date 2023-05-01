using AutoMapper;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.PhotoDto;
using Otiva.Domain;
using Otiva.Domain.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.MapProfile
{
    public class PhotoMapProfile : Profile
    {
        public PhotoMapProfile()
        {
            CreateMap<PhotoAds, InfoPhotoResponse>().ReverseMap();
            CreateMap<PhotoUsers, InfoPhotoResponse>().ReverseMap();
        }
    }
}
