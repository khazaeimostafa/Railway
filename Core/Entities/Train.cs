using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Train : BaseEntity
    {
        [Required]
        public string Name { get; set; } // varchar 50 not null

        [Required]
        public int Capacity { get; set; } // Not Null

        [Required]
        public string Status { get; set; } // 50 Not Null

        public Travel Travel { get; set; }

    }
}
 