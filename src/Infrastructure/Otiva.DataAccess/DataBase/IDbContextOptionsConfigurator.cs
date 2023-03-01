using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.DataBase
{
    public interface IDbContextOptionsConfigurator<TContext> where TContext : DbContext
    {
        void Configure(DbContextOptionsBuilder<TContext> opions);
    }
}
