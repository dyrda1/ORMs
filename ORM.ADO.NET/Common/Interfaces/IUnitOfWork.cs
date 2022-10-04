using System;
using System.Threading.Tasks;

namespace ORM.ADO.NET.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }

        public Task Save();
    }
}
