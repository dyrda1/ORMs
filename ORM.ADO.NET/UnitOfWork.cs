using Microsoft.Data.SqlClient;
using ORM.ADO.NET.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace ORM.ADO.NET
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUserRepository _userRepository;
        private readonly SqlTransaction _transaction;

        public UnitOfWork(IUserRepository userRepository, SqlTransaction transaction)
        {
            _userRepository = userRepository;
            _transaction = transaction;
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
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
            }
        }

        public void Dispose()
        {
            _transaction.Connection?.Dispose();
            _transaction.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
