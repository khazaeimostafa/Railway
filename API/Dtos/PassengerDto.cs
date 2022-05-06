using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PassengerDto
    {
        public string Name { get; set; } // varchar 50 not null

        public string Address { get; set; } // varchar 50 Not Null

        public string Gender { get; set; } // varchar 10 Not Null

        public string Nat { get; set; } // 10 varchar

        public string Phone { get; set; } // varchar 50 Not Null
    }
}
