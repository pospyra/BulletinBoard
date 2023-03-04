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
    public class UserRepository : IUserRepository
    {
        public readonly IBaseRepository<User> _baseRepository;

        public UserRepository(IBaseRepository<User> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task Add(User model)
        {
            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(User del)
        {
            await _baseRepository.DeleteAsync(del);
        }

        public async Task EditAdAsync(User edit)
        {
            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<User> FindByIdAsync(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public IQueryable<User> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
