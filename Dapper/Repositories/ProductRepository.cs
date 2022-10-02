using Dapper;
using Microsoft.Data.SqlClient;
using ORM.Dapper.Common.Interfaces;
using ORMs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace ORM.Dapper.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public ProductRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
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

        public async Task CreateRange(IEnumerable<Product> products)
        {
            await _transaction.BulkActionAsync(x => x.BulkInsert(products));
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

        public async Task UpdateRange(IEnumerable<Product> products)
        {
            await _transaction.BulkActionAsync(x => x.BulkUpdate(products));
        }

        public async Task Delete(Product product)
        {
            await _connection.ExecuteAsync
                (
                    $"DELETE products WHERE id = @{nameof(product.Id)}",
                    product,
                    _transaction
                );
        }

        public async Task DeleteRange(IEnumerable<Product> products)
        {
            await _transaction.BulkActionAsync(x => x.BulkDelete(products));
        }
    }
}
