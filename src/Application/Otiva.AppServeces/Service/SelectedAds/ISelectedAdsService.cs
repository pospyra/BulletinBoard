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
        Task<InfoSelectedResponse> AddSelectedAsync (Guid AdId, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoSelectedResponse>> GetSelectedUsersAsync(Guid UserId, int take, int skip);

        Task DeleteAsync(Guid Id);
    }
}
