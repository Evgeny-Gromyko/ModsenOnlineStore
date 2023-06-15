using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;

public interface IProductTypeRepository
{
    Task<List<ProductType>> GetAllProductTypes();
    
    Task<ProductType> GetSingleProductType(int id);
    
    Task<ProductType> AddProductType(ProductType productType);
    
    Task<ProductType> UpdateProductType(int id, ProductType productType);
    
    Task<ProductType> DeleteProductType(int id);
}
