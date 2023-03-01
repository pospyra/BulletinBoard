using AutoMapper;
using Otiva.Contracts.CategoryDto;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.MapProfile
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile() 
        {
            CreateMap<Category, InfoCategoryResponse>().ReverseMap();

        }

    }
}
