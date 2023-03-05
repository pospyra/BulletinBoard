using AutoMapper;
using Otiva.Contracts.MessageDto;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.MapProfile
{
    public class MessageMapProfile : Profile
    {
        public MessageMapProfile() 
        {
            CreateMap<InfoMessageResponse, Message>().ReverseMap();
            CreateMap<PostMessageRequest, Message>().ReverseMap();
        }  
    }
}
