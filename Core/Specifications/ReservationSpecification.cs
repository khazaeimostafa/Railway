using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ReservationSpecification : BaseSpecifcation<Reservation>
    {
        public ReservationSpecification(
            ReservationSpecParams reservationParams
        ) :
            base(
                x =>
                    (x.IsActive == reservationParams.IsActive) &&
                    (
                    !reservationParams.TravelId.HasValue ||
                    x.TravelId == reservationParams.TravelId
                    ) &&
                    (
                    !reservationParams.PassengerId.HasValue ||
                    x.PassengerId == reservationParams.PassengerId
                    )
            )
        {
            AddInclude(x => x.Travel);
            AddInclude(x => x.Passenger);

            ApplyPaging(reservationParams.PageSize *
            (reservationParams.PageIndex - 1),
            reservationParams.PageSize);
        }

        public ReservationSpecification(int id) :
            base(x => x.Id == id)
        {
            AddInclude(x => x.Travel);
            AddInclude(x => x.Passenger);
        }
    }
}
