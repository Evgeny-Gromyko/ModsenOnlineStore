using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;

public interface IProductTypeService
{
    Task<ResponseInfo> GetAllProductTypes();
    
    Task<ResponseInfo> GetSingleProductType(int id);
    
    Task<ResponseInfo> AddProductType(AddUpdateProductTypeDTO productTypeDto);
    
    Task<ResponseInfo> UpdateProductType(int id, AddUpdateProductTypeDTO productTypeDto);
    
    Task<ResponseInfo> DeleteProductType(int id);
}