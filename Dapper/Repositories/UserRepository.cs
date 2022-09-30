using Dapper;
using Microsoft.Data.SqlClient;
using ORM.Dapper.Common;
using ORM.Dapper.Entities;
using ORM.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.Dapper.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task Add(User user)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);

            await connection.ExecuteAsync
                (
                    $"INSERT INTO users (username) VALUES(@{nameof(user.Username)})", 
                    user.Username
                );
        }

        public async Task Add(User user, DbTransaction transaction)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);

            await connection.ExecuteAsync
                (
                    $"INSERT INTO users (username) VALUES(@{nameof(user.Username)})", 
                    user.Username, 
                    transaction
                );
        }

        public async Task Delete(Guid id)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);

            await connection.ExecuteAsync
                (
                    $"DELETE users WHERE id = @{nameof(id)}", 
                    id
                );
        }

        public async Task Delete(Guid id, DbTransaction transaction)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);

            await connection.ExecuteAsync
                (
                    $"DELETE users WHERE id = @{nameof(id)}", 
                    id, 
                    transaction
                );
        }

        public async Task<IEnumerable<User>> GetAllWithMessages()
        {
            using var connection = new SqlConnection("Server=DESKTOP-5SMC35H;Database=shop;Trusted_Connection=True;TrustServerCertificate=true;");
            IEnumerable<User> users = new List<User>();
            try
            {

                users = connection.Query<User>("SELECT * FROM users").ToList();
            }
            catch (Exception c)
            {
            }

            return users;
        }

        public async Task Update(User user)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);

            await connection.ExecuteAsync
                (
                    $"UPDATE users SET username = @{nameof(user.Username)} WHERE id = @{nameof(user.Id)}", 
                    user.Id
                );
        }

        public async Task Update(User user, DbTransaction transaction)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);

            await connection.ExecuteAsync
                (
                    $"UPDATE users SET username = @{nameof(user.Username)} WHERE id = @{nameof(user.Id)}", 
                    user.Id, 
                    transaction
                );
        }
    }
}
