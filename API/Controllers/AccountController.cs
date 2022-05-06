using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos.Identity;
using API.Extensions;
using API.ManagerClasses;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppIdentityDbContext context;

        private readonly UserManager<UserBase> userManager;

        private readonly SignInManager<UserBase> signInManager;

        private readonly ITokenService tokenService;

        private readonly IMapper mapper;

        public AccountController(
            AppIdentityDbContext context,
            UserManager<UserBase> userManager,
            SignInManager<UserBase> signInManager,
            ITokenService tokenService,
            IMapper mapper
        )
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }


        //"displayName": "Messi",
        //"email": "Mostafa@gmail.com",
        //"password": "Pa$$w0rd"


        [HttpPost("login")]
        public async Task<ActionResult<UserAuthBase>> Login(LoginDto appUser)
        {
            IActionResult ret = null;
            UserAuthBase auth = new UserAuthBase();
            MYSecurityManager mgr =
                new MYSecurityManager(context,
                    auth,
                    userManager,
                    signInManager,
                    tokenService);

            return auth =
                await mgr.ValidateUser(appUser.Email, appUser.Password);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserAuthBase>>
        Register([FromBody] RegisterDto registerDto)
        {
            IActionResult ret = null;
            UserAuthBase auth = new UserAuthBase();

            if (await userManager.CheckEmailExistsAsync(registerDto.Email))
            {
                ret =
                    StatusCode(StatusCodes.Status404NotFound,
                    "Email address is in use");
                return new BadRequestObjectResult(ret);
            }

            var user =
                new UserBase {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email
                };

            var result =
                await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                ret = StatusCode(StatusCodes.Status400BadRequest, auth);

                return BadRequest(ret);
            }
            else
            {
                UserBase ub =
                    await userManager.FindByEmailAsync(registerDto.Email);
                IList<Claim> claiming = await userManager.GetClaimsAsync(ub);

                await userManager
                    .AddClaimAsync(ub, new Claim("CanEdit", "True"));

                await context.SaveChangesAsync();

                auth.UserId = ub.Id;
                auth.UserName = ub.Email;
                auth.IsAuthenticated = true;

                auth.Claims = (await userManager.GetClaimsAsync(ub)).ToList();

                // context.Claims.Where(u => u.UserId == user.Id).ToList();
                auth.BearerToken = tokenService.CreateToken(auth.Claims, user);
            }
            return Ok(auth);
        }

        [HttpGet("UserClaims")]
        public async Task<IList<Claim>> UserClaims(string Email)
        {
            UserBase ub = await userManager.FindByEmailAsync(Email);
            List<Claim> claims =
                (await userManager.GetClaimsAsync(ub)).ToList();

            System.Console.WriteLine();
            return claims;
        }

        [HttpPost("ClaimAdd")]
        public async Task<ActionResult<IList<Claim>>>
        ClaimAdd(string Email, string Type, string Value)
        {
            UserBase ub = await userManager.FindByEmailAsync(Email);

            IdentityResult result =
                await userManager.AddClaimAsync(ub, new Claim(Type, Value));
            if (!result.Succeeded)
            {
                return BadRequest("Can Add claim");
            }
            List<Claim> claims =
                (await userManager.GetClaimsAsync(ub)).ToList();

            return claims;
        }

        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserAuthBase>> GetCurrentUser()
        {
            var user = await userManager.FindUserByEmail(HttpContext.User);

            UserAuthBase auth =
                new UserAuthBase {
                    Claims = (await userManager.GetClaimsAsync(user)).ToList(),
                    UserId = user.Id,
                    UserName = user.Email,
                    IsAuthenticated = true
                    // context.Claims.Where(u => u.UserId == user.Id).ToList();
                };
            auth.BearerToken = tokenService.CreateToken(auth.Claims, user);
            return auth;
        }
    }
}
