using Dapper;
using Microsoft.Data.SqlClient;
using ORM.Dapper.Common;
using ORMs.Domain.Entities;
using ORM.Dapper.Interfaces;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ORM.Dapper.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<IEnumerable<User>> GetAllWithComments()
        {
            using var connection = new SqlConnection(_connectionString);

            var users = await connection.QueryAsync<User, Comment, User>
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
            using var connection = new SqlConnection(_connectionString);

            var users = await connection.QueryAsync<User>
                (
                    $"SELECT * FROM users WHERE username LIKE @{nameof(username)}",
                    new { username = "%" + username + "%" }
                );

            return users;
        }

        public async Task<User> Get(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);

            var user = await connection.QuerySingleOrDefaultAsync<User>
                    (
                        $"SELECT * FROM users WHERE id = @{nameof(id)}",
                        new { id }
                    );

            return user;
        }

        public async Task Add(User user, DbTransaction transaction = null)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync
                (
                    $"INSERT INTO users (username) VALUES(@{nameof(user.Username)})",
                    user,
                    transaction
                );
        }

        public async Task Update(User user, DbTransaction transaction = null)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync
                (
                    $"UPDATE users SET username = @{nameof(user.Username)} WHERE id = @{nameof(user.Id)}",
                    user,
                    transaction
                );
        }

        public async Task Delete(Guid id, DbTransaction transaction = null)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync
                (
                    $"DELETE users WHERE id = @{nameof(id)}",
                    new { id },
                    transaction
                );
        }
    }
}
