using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Infrastructure.BaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<IQueryable<TEntity>> GetAllAsync();

        IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> GetByIdAsync(Guid? Id);

        Task AddAsync(TEntity model);

        Task UpdateAsync(TEntity model);
        Task DeleteAsync(TEntity model);
    }
}
