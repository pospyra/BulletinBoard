using Otiva.Domain;
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
        Task<User> FindWhere(Expression<Func<User, bool>> predicate);

        Task<User> FindByIdAsync(Guid id);

        IQueryable<User> GetAll();

        Task Add(User model);

        Task DeleteAsync(User model);

        Task EditAdAsync(User model);
    }
}
