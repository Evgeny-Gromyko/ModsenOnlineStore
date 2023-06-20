using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();

        Task<Product?> GetProductById(int id);

        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        Task RemoveProductById(int id);
    }
}
