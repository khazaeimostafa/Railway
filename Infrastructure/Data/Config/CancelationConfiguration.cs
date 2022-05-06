using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class
    CancelationConfiguration
    : IEntityTypeConfiguration<Cancelation>
    {
        public void Configure(EntityTypeBuilder<Cancelation> builder)
        {
            builder.Property(x => x.CancDate).IsRequired();
        }
    }
}
