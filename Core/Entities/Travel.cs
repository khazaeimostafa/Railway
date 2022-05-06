using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Travel : BaseEntity
    {
        public Travel()
        {
            Reservation = new List<Reservation>();
        }

        [Required]
        public DateTime Date { get; set; } //  Not Null

        [Required]
        public string Src { get; set; } //  varchar(50) Not Null

        [Required]
        public string Dest { get; set; } //  varchar(50) Not Null

        [Required]
        public int Cost { get; set; } // Not Null

        [Required]
        public int TrainId { get; set; } //  Not Null

        public Train Train { get; set; }

        public List<Reservation> Reservation { get; set; }
    }
}
