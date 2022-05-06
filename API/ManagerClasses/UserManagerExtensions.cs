using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.ManagerClasses
{
    public static class UserManagerExtensions
    {
        public static async Task<UserBase>
        FindUserByEmail(this UserManager<UserBase> userManager, ClaimsPrincipal user)
        {
            var email =
                user?
                    .Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.Email)?
                    .Value;

            return await userManager
                .Users
                .SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
