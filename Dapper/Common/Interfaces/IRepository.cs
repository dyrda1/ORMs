using System;
using System.Threading.Tasks;

namespace ORM.Dapper.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<Guid> Create(T entity);

        Task<T> Get(Guid id);

        Task Update(T entity);

        Task Delete(Guid id);
    }
}
