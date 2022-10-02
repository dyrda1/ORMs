using System;
using System.Threading.Tasks;

namespace ORM.Dapper.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductParametersRepository ProductsParameters { get; }

        public IProductRepository Products { get; }

        public IUserRepository Users { get; }

        public Task Save();
    }
}
