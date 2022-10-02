using ORM.Dapper.Common.Interfaces;
using System;
using System.Data;

namespace ORM.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbTransaction _dbTransaction;
        private readonly IUserRepository _userRepository; 

        public UnitOfWork
            (
                IDbTransaction dbTransaction,
                IUserRepository userRepository
            )
        {
            _dbTransaction = dbTransaction;
            _userRepository = userRepository;
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
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
