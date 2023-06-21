using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;

public interface IProductTypeService
{
    Task<DataResponseInfo<List<GetProductTypeDTO>>> GetAllProductTypes();
    
    Task<DataResponseInfo<GetProductTypeDTO>> GetSingleProductType(int id);
    
    Task<ResponseInfo> AddProductType(AddUpdateProductTypeDTO productTypeDto);
    
    Task<ResponseInfo> UpdateProductType(int id, AddUpdateProductTypeDTO productTypeDto);
    
    Task<ResponseInfo> DeleteProductType(int id);
}