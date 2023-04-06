using Otiva.Domain.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository.Photos
{
    public interface IPhotoUsersRepository
    {
        Task<PhotoUsers> FindByIdAsync(Guid id, CancellationToken cancellation);

        IQueryable<PhotoUsers> GetAll(CancellationToken cancellation);

        Task Add(PhotoUsers model, CancellationToken cancellation);

        Task DeleteAsync(PhotoUsers photo, CancellationToken cancellation);

        Task UpdatePhotoAsync(PhotoUsers photo, CancellationToken cancellation);
    }
}
