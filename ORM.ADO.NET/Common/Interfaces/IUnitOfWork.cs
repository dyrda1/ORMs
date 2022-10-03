using System;

namespace ORM.ADO.NET.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }

        public void Save();
    }
}
