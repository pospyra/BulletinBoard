using Microsoft.EntityFrameworkCore;
using Otiva.DataAccess.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Migrations
{
   public class MigrationsDbContext : OtivaContext
{
    public MigrationsDbContext(DbContextOptions<MigrationsDbContext> options) : base(options)
    {
    }
}
}
