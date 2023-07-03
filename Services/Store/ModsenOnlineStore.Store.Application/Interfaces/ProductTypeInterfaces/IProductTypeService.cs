using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;

public interface IProductTypeService
{
    Task<DataResponseInfo<List<GetProductTypeDTO>>> GetAllProductTypesAsync();
    
    Task<DataResponseInfo<GetProductTypeDTO>> GetSingleProductTypeAsync(int id);
    
    Task<ResponseInfo> AddProductTypeAsync(AddUpdateProductTypeDTO productTypeDto);
    
    Task<ResponseInfo> UpdateProductTypeAsync(int id, AddUpdateProductTypeDTO productTypeDto);
    
    Task<ResponseInfo> DeleteProductTypeAsync(int id);
}