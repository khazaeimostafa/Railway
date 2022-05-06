using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Passenger : BaseEntity
    {
        public Passenger()
        {
            Reservation = new List<Reservation>();
        }

        [Required]
        public string Name { get; set; } // varchar 50 not null

        [Required]
        public string Address { get; set; } // varchar 50 Not Null

        [Required]
        public string Gender { get; set; } // varchar 10 Not Null

        [Required]
        public string Nat { get; set; } // 10 varchar

        [Required]
        public string Phone { get; set; } // varchar 50 Not Null

        public List<Reservation> Reservation { get; set; }
    }
}
