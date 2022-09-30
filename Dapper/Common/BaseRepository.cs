using Microsoft.Extensions.Configuration;

namespace ORM.Dapper.Common
{
    public abstract class BaseRepository
    {
        protected readonly string _connectionString;

        public BaseRepository()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            _connectionString = config.GetConnectionString("Default");
        }
    }
}
