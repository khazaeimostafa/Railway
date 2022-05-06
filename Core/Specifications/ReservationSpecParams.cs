using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ReservationSpecParams : BaseSpecParams
    {
        public int? PassengerId { get; set; } // Not Null

        public int? TravelId { get; set; } // Not Null

        public string IsActive { get; set; }
    }
}
