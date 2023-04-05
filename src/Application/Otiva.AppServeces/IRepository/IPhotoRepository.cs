using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface IPhotoRepository
    {
        Task<Photo> FindByIdAsync(Guid id, CancellationToken cancellation);

        IQueryable<Photo> GetAll( CancellationToken cancellation);

        Task Add(Photo model, CancellationToken cancellation);

        Task DeleteAsync(Photo photo, CancellationToken cancellation);

        Task UpdatePhotoAsync(Photo photo, CancellationToken cancellation);
    }
}
