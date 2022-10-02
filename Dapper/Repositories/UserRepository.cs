using Dapper;
using Microsoft.Data.SqlClient;
using ORMs.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ORM.Dapper.Common.Interfaces;
using Z.Dapper.Plus;
using System.Data;

namespace ORM.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;
        private readonly IDbTransaction _transaction;

        public UserRepository(SqlConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;

            //DapperPlusManager.Entity<User>("UserMapping")
            //    .Table("users")
            //    .IgnoreOnMergeInsert(x => x.Id)
            //    .Key(x => x.Id, "id")
            //    .Map(x => x.Username, "username")
            //    .Output(x => x.Id, "id")
            //    .AfterAction((action, user) =>
            //    {
            //        if (action == DapperPlusActionKind.Insert)
            //        {

            //        }
            //    });
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
                    }, 
                    transaction: _transaction
                );

            return users;
        }

        public async Task<IEnumerable<User>> GetWhereUsernameLike(string username)
        {
            var users = await _connection.QueryAsync<User>
                (
                    $"SELECT * FROM users WHERE username LIKE @{nameof(username)}",
                    new { username = "%" + username + "%" },
                    transaction: _transaction
                );

            return users;
        }

        public async Task<User> Get(Guid id)
        {
            var user = await _connection.QuerySingleOrDefaultAsync<User>
                (
                    $"SELECT * FROM users WHERE id = @{nameof(id)}",
                    new { id },
                    transaction: _transaction
                );

            return user;
        }

        public async Task Create(User user)
        {
            var id = await _connection.ExecuteScalarAsync<Guid>
                (
                    $"DECLARE @IDENTITY UNIQUEIDENTIFIER; SET @IDENTITY = NEWID(); INSERT INTO users (id, username) VALUES(@IDENTITY, @{nameof(user.Username)}); SELECT @IDENTITY",
                    user,
                    _transaction
                );

            foreach (var order in user.Orders)
            {
                order.UserId = id;
            }
            foreach (var cart in user.Carts)
            {
                cart.UserId = id;
            }
            foreach (var comment in user.Comments)
            {
                comment.UserId = id;
            }
        }
        
        public async Task CreateRange(IEnumerable<User> users)
        {
            await _transaction.BulkActionAsync(x => x.BulkInsert("UserMapping", users));
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
            await _transaction.BulkActionAsync(x => x.BulkUpdate("UserMapping", users));
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
            await _transaction.BulkActionAsync(x => x.BulkDelete("UserMapping", users));
        }
    }
}
