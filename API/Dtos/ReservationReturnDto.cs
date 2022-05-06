using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ReservationReturnDto
    {
        public int PassengerId { get; set; } // Not Null

        public int TravelId { get; set; } // Not Null

        public string IsActive { get; set; }

        public string Passenger { get; set; }
    }
}
