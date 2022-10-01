using ORM.Dapper.Common.Interfaces;
using System.Data;

namespace ORM.Dapper.Common
{
    public class UnitofWork : IUnitOfWork
    {
        private readonly IDbTransaction _dbTransaction; //readonly?
        private readonly IProductParametersRepository _productParametersRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository; 

        public UnitofWork
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

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
                _dbTransaction.Connection.BeginTransaction();
            }
            catch
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}
