using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ReservationDto
    {
        public int PassengerId { get; set; } // Not Null

        public int TravelId { get; set; } // Not Null

        public string IsActive { get; set; }

        public string Passenger { get; set; }

        public string Travel { get; set; }

        public int CancelationId { get; set; }
    }
}
