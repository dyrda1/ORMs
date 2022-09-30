using ORM.Dapper.Common;
using ORMs.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORM.Dapper.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithComments();
    }
}
