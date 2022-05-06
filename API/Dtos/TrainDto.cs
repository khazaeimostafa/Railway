using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class TrainDto
    {
        [Required]
        public string Name { get; set; } // varchar 50 not null

        [Required]
        public int Capacity { get; set; } // Not Null

        [Required]
        public string Status { get; set; } // 50 Not Null

        public int? TravelId { get; set; }

    }
}
 