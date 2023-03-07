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
        Task<Photo> FindByIdAsync(Guid id);

        IQueryable<Photo> GetAll();

        Task Add(Photo model);

        Task DeleteAsync(Photo photo);

        Task UpdatePhotoAsync(Photo photo);
    }
}
