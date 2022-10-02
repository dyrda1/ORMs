using ORM.Dapper.Common.Interfaces;
using System.Data;

namespace ORM.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbTransaction _dbTransaction;
        private readonly IProductParametersRepository _productParametersRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository; 

        public UnitOfWork
            (
                IDbTransaction dbTransaction,
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

        public void Save()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            _dbTransaction.Dispose();
        }
    }
}
