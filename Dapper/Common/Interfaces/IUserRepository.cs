using ORMs.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORM.Dapper.Common.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithMessages();

        Task<IEnumerable<User>> GetWhereUsernameLike(string username);
    }
}
