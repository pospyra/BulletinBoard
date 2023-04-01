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

        Task<DomainUser> FindByIdAsync(Guid id);

        IQueryable<DomainUser> GetAll();

        Task Add(DomainUser model);

        Task DeleteAsync(DomainUser model);

        Task EditAdAsync(DomainUser model);
    }
}
