using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Config
{
    public class
    ReservationConfiguration
    : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(
            Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Reservation
            >
            builder
        )
        {
            // builder.Property(p => p.Pid).IsRequired();
            // builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            // builder.Property(x => x.TravSrc).IsRequired().HasMaxLength(50);
            // builder.Property(x => x.TravDest).IsRequired().HasMaxLength(50);
            // builder.Property(c => c.TravCost).IsRequired();
            // builder.HasKey(x => new { x.PassengerId, x.TravelId });
            // builder.HasAlternateKey(x => x.Code);
            // builder.HasOne(x => x.Cancelation).WithOne(x => x.Reservation).IsRequired();
            // builder.HasKey(x => new { x.PassengerId, x.TravelId });
            
            builder.HasKey(x => x.Id);
            builder.Property(c => c.PassengerId).IsRequired();
            builder.Property(x => x.TravelId).IsRequired();
        }
    }
}
