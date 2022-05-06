using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class TrainConfiguration : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
          
          
            builder.Property(p => p.Id).IsRequired();

            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Capacity).IsRequired();

            builder.Property(p => p.Status).IsRequired().HasMaxLength(50);
            builder.HasOne(x => x.Travel).WithOne(x => x.Train);

        }
    }
}
