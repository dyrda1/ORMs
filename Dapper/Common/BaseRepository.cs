using System.Configuration;

namespace ORM.Dapper.Common
{
    public abstract class BaseRepository
    {
        protected readonly ConnectionStringSettings _connection = ConfigurationManager.ConnectionStrings["Default"];
    }
}
