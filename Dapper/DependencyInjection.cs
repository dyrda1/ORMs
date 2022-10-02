using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ORM.Dapper.Common.Interfaces;
using ORM.Dapper.Repositories;
using System.Data;

namespace ORM.Dapper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDapper(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddScoped(x =>
            {
                var config = x.GetRequiredService<IConfiguration>();
                var connection = new SqlConnection(config.GetConnectionString("Default"));

                return connection;
            });

            services.AddScoped<IDbTransaction>(s =>
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
