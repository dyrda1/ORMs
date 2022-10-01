using Dapper;
using Microsoft.Data.SqlClient;
using ORM.Dapper.Common.Interfaces;
using ORMs.Domain.Entities;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ORM.Dapper.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _connection;
        private readonly IDbTransaction _transaction;

        public ProductRepository(SqlConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<Guid> Create(Product product)
        {
            var id = await _connection.ExecuteScalarAsync<Guid>
                (
                    $"DECLARE @IDENTITY UNIQUEIDENTIFIER; SET @IDENTITY = NEWID(); INSERT INTO products (id, name, description, image) VALUES(@IDENTITY, @{nameof(product.Name)}, @{nameof(product.Description)}, @{nameof(product.Image)}); SELECT @IDENTITY",
                    product,
                    _transaction
                );

            return id;
        }

        public async Task<Product> Get(Guid id)
        {
            var product = await _connection.QuerySingleOrDefaultAsync<Product>
                (
                    $"SELECT * FROM products WHERE id = @{nameof(id)}",
                    new { id }
                );

            return product;
        }

        public async Task Update(Product product)
        {
            await _connection.ExecuteAsync
                (
                    $"UPDATE products SET name = @{nameof(product.Name)}, description = @{nameof(product.Description)}, image = @{nameof(product.Image)} WHERE id = @{nameof(product.Id)}",
                    product,
                    _transaction
                );
        }

        public async Task Delete(Guid id)
        {
            await _connection.ExecuteAsync
                (
                    $"DELETE products WHERE id = @{nameof(id)}",
                    new { id },
                    _transaction
                );
        }
    }
}
