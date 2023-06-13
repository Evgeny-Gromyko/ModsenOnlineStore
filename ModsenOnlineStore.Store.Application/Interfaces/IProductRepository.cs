using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<List<Product>> AddProduct(Product product);
        Task<List<Product>> UpdateProduct(Product product);
        Task<List<Product>> RemoveProductById(int id);
    }
}
