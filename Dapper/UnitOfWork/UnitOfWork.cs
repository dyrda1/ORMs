using Microsoft.Data.SqlClient;
using ORM.Dapper.Common.Interfaces;
using System.Threading.Tasks;

namespace ORM.Dapper.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlTransaction _dbTransaction;
        private readonly IProductParametersRepository _productParametersRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository; 

        public UnitOfWork
            (
                SqlTransaction dbTransaction,
                IUserRepository userRepository,
                IProductRepository productRepository,
                IProductParametersRepository productParametersRepository
            )
        {
            _dbTransaction = dbTransaction;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _productParametersRepository = productParametersRepository;
        }

        public IProductParametersRepository ProductsParameters
        {
            get
            {
                return _productParametersRepository;
            }
        }

        public IProductRepository Products
        {
            get
            {
                return _productRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                return _userRepository;
            }
        }

        public async Task Save()
        {
            try
            {
                await _dbTransaction.CommitAsync();
            }
            catch
            {
                await _dbTransaction.RollbackAsync();
            }
        }

        public void Dispose()
        {
            _dbTransaction.Dispose();
        }
    }
}
