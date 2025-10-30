using Microsoft.AspNetCore.Cors.Infrastructure;
using MudBlazor.Services;
using PostOfficeFrontendProject__all_interactive.Helper;
using PostOfficeFrontendProject__all_interactive.Interface;
using PostOfficeFrontendProject__all_interactive.Middelware;

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

            services.AddMudServices();
            services.AddScoped<IDataPassHelper, DataPassHelper>();


            return services;
        }
    }
}
