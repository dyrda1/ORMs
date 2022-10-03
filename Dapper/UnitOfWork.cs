using Microsoft.Data.SqlClient;
using ORM.Dapper.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace ORM.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlTransaction _dbTransaction;
        private readonly IUserRepository _userRepository; 

        public UnitOfWork
            (
                SqlTransaction dbTransaction,
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
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
