using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class TravelSpecParams : BaseSpecParams
    {
        public DateTime? Date { get; set; }

        public string Src { get; set; }

        public string Dest { get; set; }

        public int? Cost { get; set; }

        public int? TrainId { get; set; }
    }
}
