using AAA.src.Application.Service;
using AAA.src.Domain.Interface;
using AAA.src.Infrastructure.Data;
using AAA.src.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AAA.src.Infrastructure.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            const string connectionName = "DefaultConnection";
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(connectionName)).AddInterceptors();
            });
            return services;
        }

        public static IServiceCollection AddAuthentication (this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            return services;
        }
    }
}
