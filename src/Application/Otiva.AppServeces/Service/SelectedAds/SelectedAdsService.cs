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
                DateAdded= DateTime.Now,
            };
            await _selectedadRepository.Add(selected);

            return _mapper.Map<InfoSelectedResponse>(selected);
        }

        public async Task DeleteAsync(Guid Id)
        {
            var selectedDel = await _selectedadRepository.FindByIdAsync(Id);
            await _selectedadRepository.DeleteAsync(selectedDel);
        }

        public async Task<IReadOnlyCollection<InfoSelectedResponse>> GetSelectedUsersAsync(Guid UserId, int take, int skip)
        {
            return await _selectedadRepository.GetAll()
               .Where(x => x.UserId == UserId)
               .Select(a=> new InfoSelectedResponse()
               {
                   Id= a.Id,
                   UserId = a.UserId,
                   AdId= a.AdId,
                   DateAdded= a.DateAdded,
               }).OrderBy(x =>x.DateAdded).Skip(skip).Take(take).ToListAsync();

        }
    }
}
