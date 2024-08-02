using APIBingo.Domain.Entities;

namespace APIBingo.Domain.IRepositories
{
    public interface IProductRepository
    {
        Task<ProductResponse> AddProduct(Product product);
    }
}
