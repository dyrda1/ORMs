using System;
using System.Threading.Tasks;

namespace ORM.Dapper.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }

        public Task Save();
    }
}
