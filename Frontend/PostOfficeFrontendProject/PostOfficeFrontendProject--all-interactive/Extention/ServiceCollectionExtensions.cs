using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using MudBlazor.Services;
using PostOfficeFrontendProject__all_interactive.Helper;
using PostOfficeFrontendProject__all_interactive.Interface;
using PostOfficeFrontendProject__all_interactive.Middelware;
using PostOfficeFrontendProject__all_interactive.Provider;
using PostOfficeFrontendProject__all_interactive.Service;
using System.Security.Claims;

namespace PostOfficeFrontendProject__all_interactive.Extention
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            //string baseAddress = "http://192.168.2.102:41019";
            string baseAddress = configuration.GetValue<string>("RepositoryAPIBaseAddress");
            string AAAbaseAddress = configuration.GetValue<string>("AAAAPIBaseAddress");

            // Register HTTP clients with manually specified base addresses
            services.AddHttpClient<IPostOfficeMiddelware,PostOfficeMiddelware>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            }); 
            services.AddHttpClient<IProductsMiddelware, ProductsMiddelware>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });         
            services.AddHttpClient<IProductDropdownMiddelware, ProductDropdownMiddelware>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });    
            services.AddHttpClient<IPostManMiddleware, PostManMiddleware>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });     
            services.AddHttpClient<ICustomerMiddleware, CustomerMiddleware>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });   
            services.AddHttpClient<ILoginMiddleware, LoginMiddleware>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IRegisterMiddleware, RegisterMiddleware>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddMudServices();
            services.AddScoped<IDataPassHelper, DataPassHelper>();
            services.AddScoped<NotificationService>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

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


    }
}
