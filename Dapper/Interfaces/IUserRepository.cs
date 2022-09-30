using ORM.Dapper.Common;
using ORM.Dapper.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORM.Dapper.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithMessages();
    }
}
