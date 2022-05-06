using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.ManagerClasses
{
    public class MYSecurityManager
    {
        private readonly AppIdentityDbContext context;

        private readonly UserAuthBase auth;

        private readonly UserManager<UserBase> userManager;

        private readonly SignInManager<UserBase> signInManager;

        private readonly ITokenService tokenService;

        public MYSecurityManager(
            AppIdentityDbContext context,
            UserAuthBase auth,
            UserManager<UserBase> userManager,
            SignInManager<UserBase> signInManager,
            ITokenService tokenService
        )
        {
            this.context = context;
            this.auth = auth;

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        protected async Task<List<Claim>> GetUserClaims(string userId)
        {
            // List<UserClaim> list = new List<UserClaim>();
            List<Claim> ls = new List<Claim>();
            try
            {
                // list = context.Claims.Where(u => u.UserId == userId).ToList();

                var lis =
                    await userManager
                        .GetClaimsAsync(await userManager.FindByIdAsync(userId));

                ls = lis.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception trying to retrieve user claims.",
                    ex);
            }

            return ls;
        }

        public async Task<UserAuthBase>
        ValidateUser(string userName, string password)
        {
            UserBase user = await userManager.FindByEmailAsync(userName);

            if (user == null)
            {
                return auth;
            }
            try
            {
                var result =
                    await signInManager
                        .CheckPasswordSignInAsync(user, password, false);

                if (!result.Succeeded)
                {
                    return auth;
                }

                auth.UserId = user.Id;
                auth.UserName = userName;
                auth.IsAuthenticated = true;

                auth.Claims = (await userManager.GetClaimsAsync(user)).ToList();
                auth.BearerToken = tokenService.CreateToken(auth.Claims, user);
                return auth;
            }
            catch (System.Exception ex)
            {
                throw new Exception("Exception while trying to retrieve user.",
                    ex);
            }
        }
    }
}
