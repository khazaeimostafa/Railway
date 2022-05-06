using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ReservationRepositoryOrigin : IIReservationRepository
    {
        private readonly StoreContext context;

        public ReservationRepositoryOrigin(StoreContext context)
        {
            this.context = context;
        }

        public async Task CreateReservation(Reservation reservation)
        {
            context.Reservations.Add (reservation);
            await context.SaveChangesAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int code)
        {
            return await context
                .Reservations
                .Where(c => c.Id == code)
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Reservation>> GetReservationsAsync()
        {
            return await context.Reservations.ToListAsync();
        }
    }
}
