using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.Service.User;
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
        public readonly IUserService _userService; 
        public readonly IMapper _mapper;
        public SelectedAdsService(ISelectedAdsRepository selectedadRepository, IMapper mapper, IUserService userService)
        {
            _selectedadRepository = selectedadRepository;
            _userService = userService;
            _mapper = mapper;
        }


        public async Task<InfoSelectedResponse> AddSelectedAsync( Guid AdId, CancellationToken cancellation)
        {
            var userId = await _userService.GetCurrentUserId(cancellation);
            var selected = new Domain.ItemSelectedAd()
            {
                AdId = AdId,
                UserId = userId,
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
