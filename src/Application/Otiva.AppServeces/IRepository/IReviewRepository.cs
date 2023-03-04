using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface IReviewRepository
    {
        Task<Review> FindByIdAsync(Guid id);

        IQueryable<Review> GetAll();

        Task Add(Review model);

        Task DeleteAsync(Review model);

        Task EditAdAsync(Review edit);
    }
}
