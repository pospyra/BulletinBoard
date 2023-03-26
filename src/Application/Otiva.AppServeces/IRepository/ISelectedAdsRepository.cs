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
        IQueryable<ItemSelectedAd> GetAll();

        Task Add(ItemSelectedAd model);

        Task DeleteAsync(ItemSelectedAd model);

        Task<ItemSelectedAd> FindByIdAsync(Guid id);
    }
}
