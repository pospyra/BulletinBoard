using AutoMapper;
using Otiva.Contracts.AdDto;
using Otiva.Contracts.SelectedAdDto;
using Otiva.Domain.Ads;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otiva.Contracts.Statistics;

namespace Otiva.AppServeces.MapProfile
{
    public class StatisticsMapProfile : Profile
    {
        public StatisticsMapProfile()
        {
            CreateMap<StatisticsTableAds, InfoAdStatisticsResponse>().ReverseMap();
        }

    }
}