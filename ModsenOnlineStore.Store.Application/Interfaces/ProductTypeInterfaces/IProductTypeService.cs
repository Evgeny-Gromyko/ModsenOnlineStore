using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;

public interface IProductTypeService
{
    Task<ResponseInfo<List<GetProductTypeDTO>>> GetAllProductTypes();
    
    Task<ResponseInfo<GetProductTypeDTO>> GetSingleProductType(int id);
    
    Task<ResponseInfo<string>> AddProductType(AddUpdateProductTypeDTO productTypeDto);
    
    Task<ResponseInfo<string>> UpdateProductType(int id, AddUpdateProductTypeDTO productTypeDto);
    
    Task<ResponseInfo<string>> DeleteProductType(int id);
}