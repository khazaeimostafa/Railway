using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            
            builder.Property(x=>x.Address).IsRequired().HasMaxLength(50);

            builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);

            builder.Property(x=>x.Gender).IsRequired().HasMaxLength(10);

            builder.Property(x=>x.Nat).IsRequired().HasMaxLength(10);

            builder.Property(x=>x.Phone).IsRequired().HasMaxLength(50);


        }
    }
}


      

       