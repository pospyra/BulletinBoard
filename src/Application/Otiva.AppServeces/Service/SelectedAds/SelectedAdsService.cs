using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ISelectedAdsRepository _selectedadRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityUserService _identityService;
        private readonly ILogger<SelectedAdsService> _logger;
        public SelectedAdsService(
            ISelectedAdsRepository selectedadRepository, 
            IIdentityUserService identityService,
            IMapper mapper,
            ILogger<SelectedAdsService> logger)
        {
            _selectedadRepository = selectedadRepository;
            _mapper = mapper;
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<InfoSelectedResponse> AddSelectedAsync(Guid AdId, CancellationToken cancellation)
        {
            var userId =  Guid.Parse(await _identityService.GetCurrentUserIdAsync(cancellation));
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
            _logger.LogInformation("Удаление объявления из избранных");

            var selectedDel = await _selectedadRepository.FindByIdAsync(Id, cancellation);
            await _selectedadRepository.DeleteAsync(selectedDel, cancellation);
        }

        public async Task<IReadOnlyCollection<InfoSelectedResponse>> GetSelectedUsersAsync(int pageNumber, int pageSize, CancellationToken cancellation)
        {
            var currentUserId = Guid.Parse(await _identityService.GetCurrentUserIdAsync(cancellation));
            return await _selectedadRepository.GetAll(cancellation)
               .Where(x => x.DomainUserId == currentUserId)
               .Select(a=> new InfoSelectedResponse()
               {
                   Id= a.Id,
                   DomainUserId = a.DomainUserId,
                   AdId= a.AdId,
                   DateAdded= a.DateAdded,
               }).OrderBy(x =>x.DateAdded).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
