using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otiva.Domain.Ads;
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

            builder.HasMany(a => a.Photos)
                .WithOne(p => p.Ad)
                .HasForeignKey(p => p.AdId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.StatisticsAds)
                .WithOne()
                .HasForeignKey<StatisticsTableAds>(s => s.AdId)
                .OnDelete(DeleteBehavior.Cascade);
        } 
    }
}
