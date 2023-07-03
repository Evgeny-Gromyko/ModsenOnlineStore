using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces
{
    public interface IProductService
    {
        Task<DataResponseInfo<List<GetProductDTO>>> GetAllProducts();

        Task<DataResponseInfo<GetProductDTO>> GetProductById(int id);

        Task<ResponseInfo> AddProduct(AddProductDTO addProductDto);

        Task<ResponseInfo> UpdateProduct(UpdateProductDTO updateProductDto);

        Task<ResponseInfo> RemoveProductById(int id);

        Task<DataResponseInfo<List<GetProductDTO>>> GetAllProductsByProductTypeId(int id);

        Task<DataResponseInfo<List<GetProductDTO>>> GetAllProductsByOrderId(int id);
    }
}
