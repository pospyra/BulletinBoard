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
        Task<Review> FindByIdAsync(Guid id, CancellationToken cancellation);

        IQueryable<Review> GetAll(CancellationToken cancellation);

        Task Add(Review model, CancellationToken cancellation);

        Task DeleteAsync(Review model, CancellationToken cancellation);

        Task EditAdAsync(Review edit, CancellationToken cancellation);
    }
}
