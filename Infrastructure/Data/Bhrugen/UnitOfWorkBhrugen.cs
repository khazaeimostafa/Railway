using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Bhrugen;

namespace Infrastructure.Data.Bhrugen
{
    public class UnitOfWorkBhrugen : IUnitOfWorkBhrugen
    {
        private readonly StoreContext context;

        public UnitOfWorkBhrugen(StoreContext context)
        {
            this.context = context;

            ReserveRepository = new ReservationRepository(context);
        }

        public IReservationRepository ReserveRepository { get; private set; }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
