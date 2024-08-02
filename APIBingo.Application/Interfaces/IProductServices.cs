using APIBingo.Application.DTOs;
using APIBingo.Domain.Entities;

namespace APIBingo.Application.Interfaces
{
    public interface IProductServices
    {
        Task<ProductResponse> AddProduct(ProductRequest ProductRequest);
    }
}
