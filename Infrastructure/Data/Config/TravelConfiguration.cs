using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class TravelConfiguration : IEntityTypeConfiguration<Travel>
    {
        public void Configure(EntityTypeBuilder<Travel> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.TrainId);
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Src).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Dest).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Cost).IsRequired();
            builder.HasOne(x => x.Train);
        }
    }
}
