using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otiva.Domain;
using Otiva.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.EntityConfiguration.Configuration
{
    public class DomainUserConfiguration : IEntityTypeConfiguration<DomainUser>
    {
        public void Configure(EntityTypeBuilder<DomainUser> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasMany(a => a.Ads)
                .WithOne(p => p.DomainUser)
                .HasForeignKey(p => p.DomainUserId)
                 .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(a => a.Photos)
                .WithOne(p => p.DomainUser)
                .HasForeignKey(p => p.DomainUserId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
