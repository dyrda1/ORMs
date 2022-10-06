using Microsoft.Data.SqlClient;
using ORM.ADO.NET.Common.Interfaces;
using ORMs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.ADO.NET.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlTransaction _transaction;
        private readonly SqlConnection _connection;

        public UserRepository(SqlTransaction transaction, SqlConnection connection)
        {
            _transaction = transaction;
            _connection = connection;
        }

        public async Task<IEnumerable<User>> GetAllWithMessages()
        {
            var users = new List<User>();

            using var command = _connection.CreateCommand();
            command.Transaction = _transaction;

            command.CommandText = "SELECT * FROM users " +
                "LEFT JOIN users_folders ON users.id = users_folders.user_id " +
                "LEFT JOIN folders ON folders.id = users_folders.folder_id " +
                "LEFT JOIN messages ON messages.folder_id = folders.id";

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var user = new User
                {
                    Id = reader.GetGuid(0),
                    Username = reader.GetString(1),
                };
                users.Add(user);

                if (!await reader.IsDBNullAsync(4))
                {
                    var folder = new Folder
                    {
                        Id = reader.GetGuid(4),
                        Name = reader.GetString(5)
                    };
                    user.Folders.Add(folder);

                    if (!await reader.IsDBNullAsync(6))
                    {
                        var message = new Message
                        {
                            Id = reader.GetGuid(6),
                            Text = reader.GetString(7),
                            FolderId = reader.GetGuid(8)
                        };
                        folder.Messages.Add(message);
                    }
                }
            }

            users = users.GroupBy(x => new { x.Id, x.Username }).Select(x => new User
            {
                Id = x.Key.Id,
                Username = x.Key.Username,
                Folders = x.SelectMany(z => z.Folders).GroupBy(z => new { z.Id, z.Name }).Select(z => new Folder
                {
                    Id = z.Key.Id,
                    Name = z.Key.Name,
                    Messages = z.SelectMany(k => k.Messages).ToList()
                })
                .ToList()
            })
            .ToList();

            return users;
        }

        public async Task<IEnumerable<User>> GetWhereUsernameLike(string username)
        {
            var users = new List<User>();

            using var command = _connection.CreateCommand();
            command.Transaction = _transaction;

            var usernameParam = new SqlParameter($"@{nameof(username)}", "%" + username + "%");
            command.Parameters.Add(usernameParam);

            command.CommandText = $"SELECT * FROM users " +
                    $"WHERE username LIKE @{nameof(username)}";

            using var reader = await command.ExecuteReaderAsync();
            while(await reader.ReadAsync())
            {
                var user = new User
                { 
                    Id = reader.GetGuid(0),
                    Username = reader.GetString(1)
                };
                users.Add(user);
            }

            return users;
        }

        public async Task<User> Get(Guid id)
        {
            var user = new User();

            using var command = _connection.CreateCommand();
            command.Transaction = _transaction;

            var idParam = new SqlParameter($"@{nameof(id)}", id);
            command.Parameters.Add(idParam);

            command.CommandText = $"SELECT * FROM users " +
                    $"WHERE id = @{nameof(id)}";

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                user.Id = reader.GetGuid(0);
                user.Username = reader.GetString(1);
            }

            return user;
        }

        public async Task Create(User user)
        {
            using var command = _connection.CreateCommand();
            command.Transaction = _transaction;

            var idParam = new SqlParameter($"@{nameof(user.Id)}", user.Id) { Direction = ParameterDirection.Output };
            var usernameParam = new SqlParameter($"@{nameof(user.Username)}", user.Username);
            command.Parameters.AddRange(new[] { idParam, usernameParam });

            command.CommandText = $"SET @{nameof(user.Id)} = NEWID(); " +
                    $"INSERT INTO users (id, username) " +
                    $"VALUES(@{nameof(user.Id)}, @{nameof(user.Username)})";

            await command.ExecuteNonQueryAsync();

            user.Id = (Guid)idParam.Value;
        }

        public async Task CreateRange(IEnumerable<User> users)
        {
            var table = new DataTable();
            var idColumn = new DataColumn("id", typeof(Guid));
            var usernameColumn = new DataColumn("username", typeof(string));
            table.Columns.AddRange(new[] { idColumn, usernameColumn });
            foreach (var user in users)
            {
                var row = new object[]
                {
                    user.Id = Guid.NewGuid(),
                    user.Username
                };
                table.Rows.Add(row);
            }

            using var bulkCopy = new SqlBulkCopy(_connection, SqlBulkCopyOptions.CheckConstraints, _transaction);
            bulkCopy.DestinationTableName = "users";

            await bulkCopy.WriteToServerAsync(table);
        }

        public async Task Update(User user)
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;

            var idParam = new SqlParameter($"@{nameof(user.Id)}", user.Id);
            var usernameParam = new SqlParameter($"@{nameof(user.Username)}", user.Username);
            command.Parameters.AddRange(new[] { idParam, usernameParam });

            command.CommandText = $"UPDATE users SET " +
                    $"username = @{nameof(user.Username)} " +
                    $"WHERE id = @{nameof(user.Id)}";

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateRange(IEnumerable<User> users)
        {
            var table = new DataTable();
            var idColumn = new DataColumn("id", typeof(Guid));
            var usernameColumn = new DataColumn("username", typeof(string));
            table.Columns.AddRange(new[] { idColumn, usernameColumn });
            foreach (var user in users)
            {
                var row = new object[]
                {
                    user.Id,
                    user.Username
                };
                table.Rows.Add(row);
            }

            using var command = _connection.CreateCommand();
            command.Transaction = _transaction;

            command.CommandText = "CREATE TABLE #TmpTable(id UNIQUEIDENTIFIER, username NVARCHAR(50))";
            await command.ExecuteNonQueryAsync();

            using var bulkCopy = new SqlBulkCopy(_connection, SqlBulkCopyOptions.CheckConstraints, _transaction);
            bulkCopy.DestinationTableName = "#TmpTable";

            await bulkCopy.WriteToServerAsync(table);

            command.CommandText = "UPDATE users SET " +
                "username = #TmpTable.username FROM users " +
                "INNER JOIN #TmpTable ON users.id = #TmpTable.id; " +
                "DROP TABLE #TmpTable";

            await command.ExecuteNonQueryAsync();
        }

        public async Task Delete(User user)
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;

            var idParam = new SqlParameter($"@{nameof(user.Id)}", user.Id);
            command.Parameters.Add(idParam);

            command.CommandText = $"DELETE users " +
                    $"WHERE id = @{nameof(user.Id)}";

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteRange(IEnumerable<User> users)
        {
            var table = new DataTable();
            var idColumn = new DataColumn("id", typeof(Guid));
            var usernameColumn = new DataColumn("username", typeof(string));
            table.Columns.AddRange(new[] { idColumn, usernameColumn });
            foreach (var user in users)
            {
                var row = new object[]
                {
                    user.Id,
                    user.Username
                };
                table.Rows.Add(row);
            }

            using var command = _connection.CreateCommand();
            command.Transaction = _transaction;

            command.CommandText = "CREATE TABLE #TmpTable(id UNIQUEIDENTIFIER, username NVARCHAR(50))";
            await command.ExecuteNonQueryAsync();

            using var bulkCopy = new SqlBulkCopy(_connection, SqlBulkCopyOptions.CheckConstraints, _transaction);
            bulkCopy.DestinationTableName = "#TmpTable";

            await bulkCopy.WriteToServerAsync(table);

            command.CommandText = "DELETE users FROM users " +
                "INNER JOIN #TmpTable ON users.id = #TmpTable.id; " +
                "DROP TABLE #TmpTable";

            await command.ExecuteNonQueryAsync();
        }
    }
}
