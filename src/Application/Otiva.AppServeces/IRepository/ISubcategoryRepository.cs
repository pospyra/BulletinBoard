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
        Task<Subcategory> FindByIdAsync(Guid id);

        IQueryable<Subcategory> GetAll();

        Task Add(Subcategory model);

        Task DeleteAsync(Subcategory model);

        Task EditSubcategoryAsync(Subcategory model);
    }
}
