using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ReservationRequestDto
    {
        public int PassengerId { get; set; } // Not Null

        public int TravelId { get; set; } // Not Null

         

        public int? CancelationId { get; set; }
    }
}
