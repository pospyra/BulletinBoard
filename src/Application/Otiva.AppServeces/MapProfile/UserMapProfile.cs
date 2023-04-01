﻿using AutoMapper;
using Otiva.Contracts.UserDto;
using Otiva.Domain.User;
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
            CreateMap<DomainUser, InfoUserResponse>().ReverseMap();
            CreateMap<DomainUser, RegistrationOrUpdateRequest>().ReverseMap();
            CreateMap<DomainUser, LoginRequest>().ReverseMap();
        }

    }
}
