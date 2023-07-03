using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces
{
    public interface IProductService
    {
        Task<DataResponseInfo<List<GetProductDTO>>> GetAllProductsAsync();

        Task<DataResponseInfo<GetProductDTO>> GetProductByIdAsync(int id);

        Task<ResponseInfo> AddProductAsync(AddProductDTO addProductDto);

        Task<ResponseInfo> UpdateProductAsync(UpdateProductDTO updateProductDto);

        Task<ResponseInfo> RemoveProductByIdAsync(int id);

        Task<DataResponseInfo<List<GetProductDTO>>> GetAllProductsByProductTypeIdAsync(int id);

        Task<DataResponseInfo<List<GetProductDTO>>> GetAllProductsByOrderIdAsync(int id);
    }
}
