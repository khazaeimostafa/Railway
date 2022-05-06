using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.ManagerClasses
{
    public static class ClaimStore
    {
        public static List<Claim>
            AllClaims =
                new List<Claim>() { new Claim("Create Role", "CreateRole") };
    }
}
