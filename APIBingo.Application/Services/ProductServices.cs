using APIBingo.Application.DTOs;
using APIBingo.Application.Interfaces;
using APIBingo.Domain.Entities;
using APIBingo.Domain.IRepositories;

namespace APIBingo.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ProductResponse> AddProduct(ProductRequest productRequest)
        {
            var response = new ProductResponse();

            try
            {
                var product = new Product
                {
                    IdCliente = productRequest.IdCliente,
                    Name = productRequest.Name,
                    Description = productRequest.Description,
                    Price = productRequest.Price,
                    Category = productRequest.Category,
                    CreationDate = DateTime.UtcNow,
                    Status = "Active",
                    CreatedBy = productRequest.CreatedBy,
                    CreatedMachine = productRequest.CreatedMachine
                };

                response = await _productRepository.AddProduct(product);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Unexpected error: {ex.Message}";
                response.StatusCode = 500;
            }

            return response;
        }
    }
}
