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
    public class ProductParametersRepository : IProductParametersRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public ProductParametersRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<ProductParameters> Get(Guid id)
        {
            var productParameters = await _connection.QuerySingleOrDefaultAsync<ProductParameters>
                (
                    $"SELECT * FROM product _parameters WHERE id = @{nameof(id)}",
                    new { id }
                );

            return productParameters;
        }

        public async Task<Guid> Create(ProductParameters productParameters)
        {
            var id = await _connection.ExecuteScalarAsync<Guid>
                (
                    $"DECLARE @IDENTITY UNIQUEIDENTIFIER; SET @IDENTITY = NEWID(); INSERT INTO products (color, memory, quantity, price, product_id) VALUES(@{nameof(productParameters.Color)}, @{nameof(productParameters.Memory)}, @{nameof(productParameters.Quantity)}, @{nameof(productParameters.Price)}, @{nameof(productParameters.ProductId)}); SELECT @IDENTITY",
                    productParameters,
                    _transaction
                );

            return id;
        }

        public async Task CreateRange(IEnumerable<ProductParameters> productsParameters)
        {
            await _transaction.BulkActionAsync(x => x.BulkInsert(productsParameters));
        }

        public async Task Update(ProductParameters productParameters)
        {
            await _connection.ExecuteAsync
                (
                    $"UPDATE product_parameters SET color = @{nameof(productParameters.Color)}, memory = @{nameof(productParameters.Memory)}, quantity = @{nameof(productParameters.Quantity)}, price = @{nameof(productParameters.Price)}, product_id = @{nameof(productParameters.ProductId)} WHERE id = @{nameof(productParameters.Id)}",
                    productParameters,
                    _transaction
                );
        }

        public async Task UpdateRange(IEnumerable<ProductParameters> productsParameters)
        {
            await _transaction.BulkActionAsync(x => x.BulkUpdate(productsParameters));
        }

        public async Task Delete(ProductParameters productParameters)
        {
            await _connection.ExecuteAsync
                (
                    $"DELETE product_parameters WHERE id = @{nameof(productParameters.Id)}",
                    productParameters,
                    _transaction
                );
        }

        public async Task DeleteRange(IEnumerable<ProductParameters> productsParameters)
        {
            await _transaction.BulkActionAsync(x => x.BulkDelete(productsParameters));
        }
    }
}
