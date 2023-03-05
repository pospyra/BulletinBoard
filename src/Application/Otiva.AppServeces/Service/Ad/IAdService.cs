using Otiva.Contracts.AdDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.Ad
{
    public interface IAdService
    {
        Task<InfoAdResponse> GetByIdAsync(Guid id);

        Task<Guid> CreateAdAsync(CreateOrUpdateAdRequest createAd, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoAdResponse>> GetAllAsync( int take, int skip);

        Task DeleteAsync(Guid id);

        Task<InfoAdResponse> EditAdAsync(Guid Id, CreateOrUpdateAdRequest editAd);

        Task<IReadOnlyCollection<InfoAdResponse>> GetByFilterAsync(SearchFilterAd search);


    }
}
