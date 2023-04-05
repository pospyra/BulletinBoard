using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Otiva.DataAccess.EntityConfiguration.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.DataBase
{
    public class OtivaContext : DbContext
    {
        public OtivaContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityUserRole<string>> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IdentityUserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityUserConfiguration());
            modelBuilder.ApplyConfiguration(new AdConfiguraration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
        }
    }
}
