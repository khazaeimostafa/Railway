using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Dtos
{
    public class TravelDto
    {
        public DateTime Date { get; set; } //  Not Null

        public string Src { get; set; } //  varchar(50) Not Null

        public string Dest { get; set; } //  varchar(50) Not Null

        public int Cost { get; set; } // Not Null

        public int TrainId { get; set; } //  Not Null

        public Train Train { get; set; }

         
    }
}
