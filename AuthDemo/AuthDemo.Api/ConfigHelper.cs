using AuthDemo.Api.Contextes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthDemo.Api
{
    public static class ConfigHelper
    {
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AuthDemoDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
            });
        }

        public static void ConfigureIdentity(this WebApplicationBuilder builder)
        {

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
            })
                .AddEntityFrameworkStores<AuthDemoDbContext>()
                .AddDefaultTokenProviders();

            AuthenticationBuilder authenticationBuilder = builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            byte[] key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value);
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(key);
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateActor = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                IssuerSigningKey = symmetricSecurityKey

            };

            authenticationBuilder.AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}
