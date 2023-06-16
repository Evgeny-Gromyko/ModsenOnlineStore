using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;

public interface IProductTypeService
{
    Task<ResponseInfo<List<GetProductTypeDTO>>> GetAllProductTypes();
    
    Task<ResponseInfo<GetProductTypeDTO>> GetSingleProductType(int id);
    
    Task<OperationResult> AddProductType(AddUpdateProductTypeDTO productTypeDto);
    
    Task<OperationResult> UpdateProductType(int id, AddUpdateProductTypeDTO productTypeDto);
    
    Task<OperationResult> DeleteProductType(int id);
}