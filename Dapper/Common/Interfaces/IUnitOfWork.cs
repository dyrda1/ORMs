using System;

namespace ORM.Dapper.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductParametersRepository ProductsParameters { get; }

        public IProductRepository Products { get; }

        public IUserRepository Users { get; }

        public void Commit();
    }
}
