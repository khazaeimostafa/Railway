using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces.Bhrugen
{
    public interface IUnitOfWorkBhrugen : IDisposable
    {
        void Save();

        IReservationRepository ReserveRepository { get; }
    }
}
