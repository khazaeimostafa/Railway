using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Identity
{
    [Table("UserClaim", Schema = "Security")]
    public class UserClaim
    {
        [Required()]
        [Key()]
        public int ClaimId { get; set; }

        [Required()]
        public string UserId { get; set; }

        [Required()]
        public string ClaimType { get; set; }

        [Required()]
        public string ClaimValue { get; set; }
    }
}
