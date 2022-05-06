using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Identity;
using StackExchange.Redis;
using Newtonsoft.Json.Serialization;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);






builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<StoreContext>(optins =>
optins.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddDbContext<AppIdentityDbContext>(x =>
   {
       x.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection"));
   });

builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);

    return ConnectionMultiplexer.Connect(configuration);
});


builder.Services.AddApplicationServices();
//  builder.Services.AddIdentityServices(builder.Configuration);
var config = builder.Configuration;
builder.Services
 .AddIdentityCore<UserBase>((x) =>
 {
     x.Password.RequireDigit = false;
     x.Password.RequiredLength = 5;
     x.Password.RequiredUniqueChars = 0;
     x.Password.RequireLowercase = false;
     x.Password.RequireNonAlphanumeric = false;
     x.Password.RequireUppercase = false;
 })
 .AddEntityFrameworkStores<AppIdentityDbContext>()
 .AddSignInManager<SignInManager<UserBase>>();
var Key = builder.Configuration.GetConnectionString("Token:Key");
var Issuer = builder.Configuration.GetConnectionString("Token:Issuer");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding
                            .UTF8
                            .GetBytes(builder.Configuration["Token:Key"])),
                ValidIssuer = builder.Configuration["Token:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = false

            };
    });

// Add services to the container.

// builder.Services.AddControllers().AddNewtonsoftJson(options =>
//     options.SerializerSettings.ReferenceLoopHandling =
//      Newtonsoft.Json.ReferenceLoopHandling.Ignore;

// );

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling =
     Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();

}

);





// tells the serializer to discard properties with null values
builder.Services.Configure<MvcNewtonsoftJsonOptions>(opts =>
{
    opts.SerializerSettings.NullValueHandling
    = Newtonsoft.Json.NullValueHandling.Ignore;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
            });
        });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();


}


app.UseSwaggerDocumentation();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod().AllowAnyOrigin()
    );

// app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
