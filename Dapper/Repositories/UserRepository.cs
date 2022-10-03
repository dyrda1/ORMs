using Dapper;
using Microsoft.Data.SqlClient;
using ORMs.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ORM.Dapper.Common.Interfaces;
using Z.Dapper.Plus;

namespace ORM.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
        private readonly DapperPlusContext _dapperPlusContext;

        public UserRepository
            (
                SqlConnection connection,
                SqlTransaction transaction,
                DapperPlusContext dapperPlusContext
            )
        {
            _connection = connection;
            _transaction = transaction;

            _dapperPlusContext = dapperPlusContext;
            _dapperPlusContext.Connection = connection;
            _dapperPlusContext.Transaction = transaction;
        }

        public async Task<IEnumerable<User>> GetAllWithMessages()
        {
            var users = await _connection.QueryAsync<User, UserFolder, Folder, Message, User>
                    (
                        "SELECT * FROM users " +
                        "LEFT JOIN users_folders ON users.id = users_folders.user_id " +
                        "LEFT JOIN folders ON folders.id = users_folders.folder_id " +
                        "LEFT JOIN messages ON messages.folder_id = folders.id",
                        (user, userFolder, folder, message) =>
                        {
                            if (userFolder != null)
                            {
                                user.UserFolders.Add(userFolder);
                                if (folder != null)
                                {
                                    userFolder.Folder = folder;
                                    if (message != null)
                                        folder.Messages.Add(message);
                                }
                            }

                            return user;
                        },
                        splitOn: "user_id, id, id",
                        transaction: _transaction
                    );

            return users;
        }

        public async Task<IEnumerable<User>> GetWhereUsernameLike(string username)
        {
            var users = await _connection.QueryAsync<User>
                (
                    $"SELECT * FROM users " +
                    $"WHERE username LIKE @{nameof(username)}",
                    new { username = "%" + username + "%" },
                    transaction: _transaction
                );

            return users;
        }

        public async Task<User> Get(Guid id)
        {
            var user = await _connection.QuerySingleOrDefaultAsync<User>
                (
                    $"SELECT * FROM users " +
                    $"WHERE id = @{nameof(id)}",
                    new { id },
                    transaction: _transaction
                );

            return user;
        }

        public async Task Create(User user)
        {
            user.Id = await _connection.ExecuteScalarAsync<Guid>
                (
                    $"DECLARE @IDENTITY UNIQUEIDENTIFIER; " +
                    $"SET @IDENTITY = NEWID(); " +
                    $"INSERT INTO users (id, username) " +
                    $"VALUES(@IDENTITY, @{nameof(user.Username)}); " +
                    $"SELECT @IDENTITY",
                    user,
                    _transaction
                );
        }

        public async Task CreateRange(IEnumerable<User> users)
        {
            await _dapperPlusContext.BulkActionAsync(x => x.BulkInsert(users));
        }

        public async Task Update(User user)
        {
            await _connection.ExecuteAsync
                (
                    $"UPDATE users SET " +
                    $"username = @{nameof(user.Username)} " +
                    $"WHERE id = @{nameof(user.Id)}",
                    user,
                    _transaction
                );
        }

        public async Task UpdateRange(IEnumerable<User> users)
        {
            await _dapperPlusContext.BulkActionAsync(x => x.BulkUpdate(users));
        }

        public async Task Delete(User user)
        {
            await _connection.ExecuteAsync
                (
                    $"DELETE users " +
                    $"WHERE id = @{nameof(user.Id)}",
                    user,
                    _transaction
                );
        }

        public async Task DeleteRange(IEnumerable<User> users)
        {
            await _dapperPlusContext.BulkActionAsync(x => x.BulkDelete(users));
        }
    }
}
