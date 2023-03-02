using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Otiva.AppServeces.IRepository;
using Otiva.Contracts.SelectedAdDto;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.SelectedAds
{
    public class SelectedAdsService : ISelectedAdsService
    {
        public readonly ISelectedAdsRepository _selectedadRepository;
        public readonly IMapper _mapper;
        public SelectedAdsService(ISelectedAdsRepository selectedadRepository, IMapper mapper)
        {
            _selectedadRepository = selectedadRepository;
            _mapper = mapper;
        }


        public async Task<InfoSelectedResponse> AddSelectedAsync(Guid UserId, Guid AdId)
        {
            var selected = new Domain.SelectedAd()
            {
                AdId = AdId,
                UserId = UserId,
            };
            await _selectedadRepository.AddAsync(selected);

            return _mapper.Map<InfoSelectedResponse>(selected);
        }

        public async Task DeleteAsync(Guid UserId, Guid AdId)
        {
            var selected =  _selectedadRepository.GetAll()
                .Where(x=> x.UserId == UserId && x.AdId == AdId);

            var delselected = new SelectedAd()
            {
                UserId = UserId,
                AdId = AdId
            };
            await _selectedadRepository.DeleteAsync(delselected);
        }

        public async Task<IReadOnlyCollection<InfoSelectedResponse>> GetSelectedUsers(Guid UserId)
        {
            return await _selectedadRepository.GetAll()
               .Where(x => x.UserId == UserId)
               .Select(a=> new InfoSelectedResponse()
               {
                   Id= a.Id,
                   UserId = a.UserId,
                   AdId= a.AdId
               }).ToListAsync();

        }
    }
}
