using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORM.Dapper.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);

        Task Create(T entity);

        Task CreateRange(IEnumerable<T> entities);

        Task Update(T entity);

        Task UpdateRange(IEnumerable<T> entities);

        Task Delete(T entity);

        Task DeleteRange(IEnumerable<T> entities);
    }
}
