using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.IRepository
{
    public interface IUserRepository
    {
        Task<User> FindById(Guid id);

        IQueryable<User> GetAll();

        Task AddAsync(User model);

        Task DeleteAsync(User model);

        Task EditAdAsync(User model);
    }
}
