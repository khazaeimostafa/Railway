using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    [Table("User", Schema = "Security")]
    public class UserBase : IdentityUser
    {
     public string DisplayName { get; set; }

    }
}
