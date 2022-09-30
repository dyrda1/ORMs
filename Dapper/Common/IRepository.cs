using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace ORM.Dapper.Common
{
    public interface IRepository<T>
    {
        Task<T> Get(Guid id);

        Task Add(T entity, DbTransaction transaction = null);

        Task Delete(Guid id, DbTransaction transaction = null);

        Task Update(T entity, DbTransaction transaction = null);
    }
}
