using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;

public interface IProductTypeRepository
{
    Task<List<ProductType>> GetAllProductTypesAsync(int pageNumber, int pageSize);
    
    Task<ProductType> GetSingleProductTypeAsync(int id);
    
    Task<ProductType> AddProductTypeAsync(ProductType productType);
    
    Task<ProductType> UpdateProductTypeAsync(int id, ProductType productType);
    
    Task<ProductType> DeleteProductTypeAsync(int id);
}
