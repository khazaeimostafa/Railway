using System.Text;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection
        AddIdentityServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services
                .AddIdentityCore<UserBase>((x) =>
                {
                    x.Password.RequireDigit = false;
                    x.Password.RequiredLength = 5;
                    x.Password.RequiredUniqueChars = 0;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireNonAlphanumeric =
                        x.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddSignInManager<SignInManager<UserBase>>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey =
                                new SymmetricSecurityKey(Encoding
                                        .UTF8
                                        .GetBytes(config["Token:Key"])),
                            ValidIssuer = config["Token:Issuer"],
                            ValidateIssuer = true,
                            ValidateAudience = false
                        };
                });

            return services;
        }
    }
}

// var builder =
//     services
//         .AddIdentityCore<UserBase>(x =>
//         {
//             // x.User.RequireUniqueEmail = true;
//             x.Password.RequireDigit = false;
//             x.Password.RequiredLength = 5;
//             x.Password.RequiredUniqueChars = 0;
//             x.Password.RequireLowercase = false;
//             x.Password.RequireNonAlphanumeric = false;
//             x.Password.RequireUppercase = false;
//         });
// builder = new IdentityBuilder(builder.UserType, builder.Services);
// builder.AddEntityFrameworkStores<AppIdentityDbContext>();
// builder.AddSignInManager<SignInManager<UserBase>>();
