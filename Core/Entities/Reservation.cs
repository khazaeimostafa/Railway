using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Reservation : BaseEntity
    {
        public int PassengerId { get; set; } // Not Null

        public int TravelId { get; set; } // Not Null

        public string IsActive { get; set; }

        public Passenger Passenger { get; set; }

        public Travel Travel { get; set; }

        public Cancelation Cancelation { get; set; }
    }
} 
