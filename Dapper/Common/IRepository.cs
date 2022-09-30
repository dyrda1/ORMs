using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace ORM.Dapper.Common
{
    public interface IRepository<T>
    {
        Task Add(T entity);

        Task Add(T entity, DbTransaction transaction);

        Task Delete(Guid id);

        Task Delete(Guid id, DbTransaction transaction);

        Task Update(T entity);

        Task Update(T entity, DbTransaction transaction);
    }
}
