using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.EntityConfiguration.Configuration
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<Domain.User.IdentityUser>{
        public void Configure(EntityTypeBuilder<Domain.User.IdentityUser> builder) 
        {
            builder.HasKey(x => x.Id);
        }
    } 
}
