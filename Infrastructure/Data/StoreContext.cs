using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) :
            base(options)
        {
        }

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Train> Trains { get; set; }

        public DbSet<Travel> Travels { get; set; }

        public DbSet<Cancelation> Cancelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder
            //     .Entity<Reservation>()
            //     .HasKey(x => new { x.PassengerId, x.TravelId });

            // modelBuilder.Entity<Reservation>().HasAlternateKey(x => x.Code);
            // modelBuilder
            //     .Entity<Reservation>()
            //     .HasOne(b => b.Cancelation)
            //     .WithOne(i => i.Reservation)
            //     .HasForeignKey<Cancelation>(b => b.Code);

            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly
                    .GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties =
                        entityType
                            .ClrType
                            .GetProperties()
                            .Where(p => p.PropertyType == typeof (decimal));
                    var dateTimeProperties =
                        entityType
                            .ClrType
                            .GetProperties()
                            .Where(p =>
                                p.PropertyType == typeof (DateTimeOffset));

                    foreach (var property in properties)
                    {
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion<double>();
                    }

                    foreach (var property in dateTimeProperties)
                    {
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}
