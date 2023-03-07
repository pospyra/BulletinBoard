using Otiva.AppServeces.IRepository;
using Otiva.Domain;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        public readonly IBaseRepository<Photo> _baseRepository;

        public PhotoRepository(IBaseRepository<Photo> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task Add(Photo model)
        {
            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Photo photo)
        {
            await _baseRepository.DeleteAsync(photo);
        }

        public async Task UpdatePhotoAsync(Photo edit)
        {
            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<Photo> FindByIdAsync(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public IQueryable<Photo> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
