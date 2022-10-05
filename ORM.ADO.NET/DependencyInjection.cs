using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ORM.ADO.NET.Common.Interfaces;
using ORM.ADO.NET.Repositories;

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

            services.AddScoped(s =>
            {
                var connection = s.GetRequiredService<SqlConnection>();
                connection.Open();
                var transaction = connection.BeginTransaction();

                return transaction;
            });

            return services;
        }
    }
}
