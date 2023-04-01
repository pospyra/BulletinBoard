﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.DataAccess.EntityConfiguration.Configuration
{
    public class AdConfiguraration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Description).HasMaxLength(800);

            builder.HasOne(x => x.DomainUser)
                .WithMany(p => p.Ads)
                .HasForeignKey(x => x.DomainUserId);
        }
    }
}
