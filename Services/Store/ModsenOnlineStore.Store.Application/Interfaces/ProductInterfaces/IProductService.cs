using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces
{
    public interface IProductService
    {
        Task<DataResponseInfo<List<GetProductDto>>> GetAllProductsAsync();

        Task<DataResponseInfo<GetProductDto>> GetProductByIdAsync(int id);

        Task<ResponseInfo> AddProductAsync(AddProductDto addProductDto);

        Task<ResponseInfo> UpdateProductAsync(UpdateProductDto updateProductDto);

        Task<ResponseInfo> RemoveProductByIdAsync(int id);

        Task<DataResponseInfo<List<GetProductDto>>> GetAllProductsByProductTypeIdAsync(int id);

        Task<DataResponseInfo<List<GetProductDto>>> GetAllProductsByOrderIdAsync(int id);
    }
}
