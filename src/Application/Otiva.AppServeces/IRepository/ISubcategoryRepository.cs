using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface ISubcategoryRepository
    {
        Task<Subcategory> FindByIdAsync(Guid id, CancellationToken cancellation);

        IQueryable<Subcategory> GetAll(CancellationToken cancellation);

        Task Add(Subcategory model, CancellationToken cancellation);

        Task DeleteAsync(Subcategory mode, CancellationToken cancellationl);

        Task EditSubcategoryAsync(Subcategory model, CancellationToken cancellation);
    }
}
