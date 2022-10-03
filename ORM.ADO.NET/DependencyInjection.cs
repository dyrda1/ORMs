using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ORM.ADO.NET
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAdoNet(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(x =>
            {
                var config = x.GetRequiredService<IConfiguration>();
                var connection = new SqlConnection(config.GetConnectionString("Default"));

                return connection;
            });

            services.AddScoped(x =>
            {
                var config = x.GetRequiredService<IConfiguration>();
                var connection = new SqlConnection(config.GetConnectionString("Default"));

                return connection;
            });

            return services;
        }
    }
}
