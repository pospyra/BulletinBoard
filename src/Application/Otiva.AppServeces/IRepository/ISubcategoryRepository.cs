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
        Task<Subcategory> FindById(Guid id);

        IQueryable<Subcategory> GetAll();

        Task AddAsync(Subcategory model);

        Task DeleteAsync(Subcategory model);

        Task EditSubcategoryAsync(Subcategory model);
    }
}
