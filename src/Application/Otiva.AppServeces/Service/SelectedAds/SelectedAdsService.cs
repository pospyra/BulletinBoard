using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.Service.IdentityService;
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
        public readonly IIdentityUserService _identityService;
        public SelectedAdsService(ISelectedAdsRepository selectedadRepository, IMapper mapper, IUserService userService, IIdentityUserService identityService)
        {
            _selectedadRepository = selectedadRepository;
            _userService = userService;
            _mapper = mapper;
            _identityService = identityService;
        }


        public async Task<InfoSelectedResponse> AddSelectedAsync(Guid AdId, CancellationToken cancellation)
        {
            var userId =  Guid.Parse(await _identityService.GetCurrentUserId(cancellation));
            var selected = new Domain.ItemSelectedAd()
            {
                AdId = AdId,
                DomainUserId = userId,
            };
            await _selectedadRepository.Add(selected, cancellation);

            return _mapper.Map<InfoSelectedResponse>(selected);
        }

        public async Task DeleteAsync(Guid Id, CancellationToken cancellation)
        {
            var selectedDel = await _selectedadRepository.FindByIdAsync(Id, cancellation);
            await _selectedadRepository.DeleteAsync(selectedDel, cancellation);
        }

        public async Task<IReadOnlyCollection<InfoSelectedResponse>> GetSelectedUsersAsync(int take, int skip, CancellationToken cancellation)
        {
            var currentUserId = Guid.Parse(await _identityService.GetCurrentUserId(cancellation));
            return await _selectedadRepository.GetAll(cancellation)
               .Where(x => x.DomainUserId == currentUserId)
               .Select(a=> new InfoSelectedResponse()
               {
                   Id= a.Id,
                   UserId = a.DomainUserId,
                   AdId= a.AdId,
                   DateAdded= a.DateAdded,
               }).OrderBy(x =>x.DateAdded).Skip(skip).Take(take).ToListAsync();
        }
    }
}
