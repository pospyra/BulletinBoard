using Microsoft.EntityFrameworkCore;
using Otiva.AppServeces.IRepository;
using Otiva.Domain.User;
using Otiva.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly IBaseRepository<DomainUser> _baseRepository;

        public UserRepository(IBaseRepository<DomainUser> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task Add(DomainUser model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(DomainUser del, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(del);
        }

        public async Task EditUserAsync(DomainUser edit, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<DomainUser> FindByIdAsync(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetByIdAsync(id);
        }

        public async Task<DomainUser> FindWhere(Expression<Func<DomainUser, bool>> predicate)
        {
            var data = _baseRepository.GetAllFiltered(predicate);

            return await data.Where(predicate).FirstOrDefaultAsync();
        }

        public IQueryable<DomainUser> GetAll(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.GetAll();
        }
    }
}
