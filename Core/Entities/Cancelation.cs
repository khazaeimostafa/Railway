using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Cancelation : BaseEntity
    {
        public DateTime CancDate { get; set; } //  Not Null

        [ForeignKey("Id")]
        public int? ReservationId { get; set; }

        public Reservation Reservation { get; set; }
    }
}
