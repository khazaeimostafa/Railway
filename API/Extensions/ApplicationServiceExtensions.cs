using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Core.Interfaces.Bhrugen;
using Infrastructure.Data;
using Infrastructure.Data.Bhrugen;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection
        AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUnitOfWorkBhrugen, UnitOfWorkBhrugen>();

            services
                .AddScoped(typeof (IGenericRepository<>),
                (typeof (GenericRepository<>)));

                
            services
                .Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var errors =
                            actionContext
                                .ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage)
                                .ToArray();

                        var errorResponse =
                            new ApiValidationErrorResponse { Errors = errors };

                        return new BadRequestObjectResult(errorResponse);
                    };
                });

            return services;
        }
    }
}
