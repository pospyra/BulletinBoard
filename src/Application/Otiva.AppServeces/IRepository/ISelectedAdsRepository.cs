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
        IQueryable<SelectedAd> GetAll();

        Task Add(SelectedAd model);

        Task DeleteAsync(SelectedAd model);

        Task<SelectedAd> FindByIdAsync(Guid id);
    }
}
