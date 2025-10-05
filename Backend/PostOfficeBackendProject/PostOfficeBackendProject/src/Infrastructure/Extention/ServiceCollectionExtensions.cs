using Microsoft.EntityFrameworkCore;
using PostOfficeBackendProject.src.Domain.Interface;
using PostOfficeBackendProject.src.Infrastructure.Data;
using PostOfficeBackendProject.src.Infrastructure.Repository;

namespace PostOfficeBackendProject.src.Infrastructure.Extention
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPostOfficeRepository, PostOfficeRepository>();


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
