using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface IAdRepository
    {
        Task<Ad> FindByIdAsync(Guid id);

        IQueryable<Ad> GetAll();

        Task Add(Ad model);

        Task DeleteAsync(Ad ad);

        Task EditAdAsync(Ad edit);
    }
}
