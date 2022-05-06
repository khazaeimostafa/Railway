using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.Bhrugen
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IEnumerable<Reservation> GetReservationsList();

        void Update(Reservation reservation);
    }
}
