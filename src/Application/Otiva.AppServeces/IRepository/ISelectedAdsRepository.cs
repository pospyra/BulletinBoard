using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface ISelectedAdsRepository
    {
        IQueryable<ItemSelectedAd> GetAll(CancellationToken cancellation);

        Task Add(ItemSelectedAd model, CancellationToken cancellation);

        Task DeleteAsync(ItemSelectedAd model, CancellationToken cancellation);

        Task<ItemSelectedAd> FindByIdAsync(Guid id, CancellationToken cancellation);
    }
}
