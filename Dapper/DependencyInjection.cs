using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ORM.Dapper.Common;
using ORM.Dapper.Common.Interfaces;
using ORM.Dapper.Repositories;
using System.Data;
using Z.Dapper.Plus;

namespace ORM.Dapper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDapper(this IServiceCollection services)
        {
            services.AddSingleton<DapperPlusContext, MessengerContext>();

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
