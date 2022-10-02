using System;

namespace ORM.Dapper.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //TODO: add repositories

        public IUserRepository Users { get; }

        public void Save();
    }
}
