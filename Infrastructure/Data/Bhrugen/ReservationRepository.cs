using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Bhrugen;

namespace Infrastructure.Data.Bhrugen
{
    public class
    ReservationRepository
    : Repository<Reservation>, IReservationRepository
    {
        private readonly StoreContext context;

        public ReservationRepository(StoreContext context) :
            base(context)
        {
            this.context = context;
        }

        public IEnumerable<Reservation> GetReservationsList()
        {
            return context.Reservations.ToList();
        }

        public void Update(Reservation reservation)
        {
            var obj =
                context
                    .Reservations
                    .FirstOrDefault(x => x.Id == reservation.Id);

            obj.Cancelation = reservation.Cancelation;
            obj.IsActive = reservation.IsActive;
            context.SaveChanges();
        }
    }
}
