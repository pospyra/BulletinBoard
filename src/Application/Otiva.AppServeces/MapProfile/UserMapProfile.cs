using AutoMapper;
using Otiva.Contracts.UserDto;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.MapProfile
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile() 
        {
            CreateMap<User, InfoUserResponse>().ReverseMap();
            CreateMap<User, RegistrationRequest>().ReverseMap();
            CreateMap<User, LoginRequest>().ReverseMap();
        }

    }
}
