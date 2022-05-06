using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<bool>
        CheckEmailExistsAsync(
            this UserManager<UserBase> userManager,
            string email
        )
        {
            return await userManager.FindByEmailAsync(email) != null;
        }
    }
}
