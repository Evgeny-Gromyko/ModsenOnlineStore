using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync(int pageNumber, int pageSize);

        Task<Product?> GetProductByIdAsync(int id);

        Task AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task RemoveProductByIdAsync(int id);

        Task<List<Product>> GetAllProductsByProductTypeIdAsync(int id, int pageNumber, int pageSize);
    }
}
