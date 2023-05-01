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
    public class StatsticAdsConfiguration : IEntityTypeConfiguration<StatisticsTableAds>
    {
        public void Configure(EntityTypeBuilder<StatisticsTableAds> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(a => a.Ads)
                .WithOne(p => p.StatisticsAds)
                .HasForeignKey(p => p.StatisticsTableAdsId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
