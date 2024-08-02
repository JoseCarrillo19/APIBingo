using APIBingo.Domain.Entities;
using APIBingo.Domain.IRepositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIBingo.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _sqlConnection;

        public ProductRepository(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection ?? throw new ArgumentNullException(nameof(sqlConnection));
        }
         
        public async Task<ProductResponse> AddProduct(Product product)
        {
            var response = new ProductResponse();
            try
            {
                _sqlConnection.Open();
                using var command = new SqlCommand("INSERT INTO Product (IdCliente, Name, Description, Price, Category, CreationDate, Status, CreatedBy, CreatedMachine) OUTPUT INSERTED.Id VALUES (@IdCliente, @Name, @Description, @Price, @Category, @CreationDate, @Status, @CreatedBy, @CreatedMachine)", _sqlConnection);
                command.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int) { Value = product.IdCliente });
                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = product.Name ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, -1) { Value = product.Description ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Value = product.Price });
                command.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar, 100) { Value = product.Category ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime) { Value = product.CreationDate });
                command.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50) { Value = product.Status ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 100) { Value = product.CreatedBy ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@CreatedMachine", SqlDbType.NVarChar, 100) { Value = product.CreatedMachine ?? (object)DBNull.Value });

                var productId = await command.ExecuteScalarAsync();

                if (productId != null)
                {
                    response.Success = true;
                    response.Message = "Product added successfully.";
                    response.ProductId = (int)productId;
                    response.ClientId = product.IdCliente;
                    response.StatusCode = 200;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error adding the product.";
                    response.StatusCode = 400; 
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Unexpected error: {ex.Message}";
                response.StatusCode = 500; 
            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;
        }
    }
}
