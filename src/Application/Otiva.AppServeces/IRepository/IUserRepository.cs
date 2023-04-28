using Otiva.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface IUserRepository
    {
        Task<DomainUser> FindWhere(Expression<Func<DomainUser, bool>> predicate);

        Task<DomainUser> FindByIdAsync(Guid id, CancellationToken cancellation);

        IQueryable<DomainUser> GetAll(CancellationToken cancellation);

        Task Add(DomainUser model, CancellationToken cancellation);

        Task DeleteAsync(DomainUser model, CancellationToken cancellation);

        Task EditUserAsync(DomainUser model, CancellationToken cancellation);
    }
}
