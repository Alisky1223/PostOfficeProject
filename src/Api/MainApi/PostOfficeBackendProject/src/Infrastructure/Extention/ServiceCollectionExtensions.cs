using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PostOfficeProject.Core.src.Application.Service;
using PostOfficeProject.Core.src.Domain.Interface;
using PostOfficeProject.Core.src.Infrastructure.Data;
using PostOfficeProject.Core.src.Infrastructure.Midleware;
using PostOfficeProject.Core.src.Infrastructure.Repository;
using System.Security.Claims;
using System.Text;

namespace PostOfficeProject.Core.src.Infrastructure.Extention
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPostOfficeRepository, PostOfficeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IPostmanRepository, PostmanRepository>();
            services.AddScoped<ITransportRepository, TransportRepository>();
            services.AddScoped<ITransportStatusRepository, TransportStatusRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<DatabaseSeederService>();

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
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

        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            const string Admin = "Admin";
            const string SuperAdmin = "SuperAdmin";
            const string Customer = "Customer";
            const string Postman = "Postman";
            const string User = "User";

            // Multi-level Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole(Admin));
                options.AddPolicy("SuperAdminPolicy", policy => policy.RequireRole(SuperAdmin));
                options.AddPolicy("CustomerPolicy", policy => policy.RequireRole(Customer));
                options.AddPolicy("PostmanPolicy", policy => policy.RequireRole(Postman));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole(User));

                options.AddPolicy("AdminOrSuperAdminPolicy", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                            (c.Type == ClaimTypes.Role && c.Value == Admin) ||
                            (c.Type == ClaimTypes.Role && c.Value == SuperAdmin))
                    ));
            });

            return services;
        }

        public static IServiceCollection AddMiddleware(this IServiceCollection services, IConfiguration configuration)
        {
            var aaaApiAddress = configuration.GetValue<string>("AAA_API_Address");

            if (string.IsNullOrWhiteSpace(aaaApiAddress))
            {
                throw new InvalidOperationException("Configuration value 'AAA_API_Address' is missing or empty. Please ensure it is set in your configuration.");
            }

            services.AddHttpClient<IUsersMiddleware, UsersMiddleware>(client =>
                client.BaseAddress = new Uri(aaaApiAddress));

            services.AddHttpClient<IAuthenticationMiddleware, AuthenticationMiddleware>(client =>
                client.BaseAddress = new Uri(aaaApiAddress));

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

    }
}
