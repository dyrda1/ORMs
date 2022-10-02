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

        public UserRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<IEnumerable<User>> GetAllWithComments()
        {
            var users = await _connection.QueryAsync<User, Comment, User>
                    (
                        "SELECT * FROM users LEFT JOIN comments ON users.id = comments.user_id",
                        (user, comment) =>
                        {
                            if (comment != null)
                                user.Comments.Add(comment);
                            return user;
                        }
                    );

            return users;
        }

        public async Task<IEnumerable<User>> GetWhereUsernameLike(string username)
        {
            var users = await _connection.QueryAsync<User>
                (
                    $"SELECT * FROM users WHERE username LIKE @{nameof(username)}",
                    new { username = "%" + username + "%" }
                );

            return users;
        }

        public async Task<User> Get(Guid id)
        {
            var user = await _connection.QuerySingleOrDefaultAsync<User>
                    (
                        $"SELECT * FROM users WHERE id = @{nameof(id)}",
                        new { id }
                    );

            return user;
        }

        public async Task<Guid> Create(User user)
        {
            var id = await _connection.ExecuteScalarAsync<Guid>
                (
                    $"DECLARE @IDENTITY UNIQUEIDENTIFIER; SET @IDENTITY = NEWID(); INSERT INTO users (id, username) VALUES(@IDENTITY, @{nameof(user.Username)}); SELECT @IDENTITY",
                    user,
                    _transaction
                );

            return id;
        }

        public async Task CreateRange(IEnumerable<User> users)
        {
            await _transaction.BulkActionAsync(x => x.BulkInsert(users));
        }

        public async Task Update(User user)
        {
            await _connection.ExecuteAsync
                (
                    $"UPDATE users SET username = @{nameof(user.Username)} WHERE id = @{nameof(user.Id)}",
                    user,
                    _transaction
                );
        }

        public async Task UpdateRange(IEnumerable<User> users)
        {
            await _transaction.BulkActionAsync(x => x.BulkUpdate(users));
        }

        public async Task Delete(User user)
        {
            await _connection.ExecuteAsync
                (
                    $"DELETE users WHERE id = @{nameof(user.Id)}",
                    user,
                    _transaction
                );
        }

        public async Task DeleteRange(IEnumerable<User> users)
        {
            await _transaction.BulkActionAsync(x => x.BulkDelete(users));
        }
    }
}
