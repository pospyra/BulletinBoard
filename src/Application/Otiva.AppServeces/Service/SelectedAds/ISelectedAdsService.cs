using Otiva.Contracts.AdDto;
using Otiva.Contracts.SelectedAdDto;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.SelectedAds
{
    public interface ISelectedAdsService
    {
        Task<InfoSelectedResponse> AddSelectedAsync(Guid UserId, Guid AdId);

        Task<IReadOnlyCollection<InfoSelectedResponse>> GetSelectedUsers(Guid UserId);

        Task DeleteAsync(Guid UserId, Guid AdId);
    }
}
