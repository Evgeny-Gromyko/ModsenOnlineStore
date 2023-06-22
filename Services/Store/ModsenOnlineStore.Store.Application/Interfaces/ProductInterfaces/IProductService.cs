using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces
{
    public interface IProductService
    {
        Task<DataResponseInfo<List<GetProductDto>>> GetAllProducts();

        Task<DataResponseInfo<GetProductDto>> GetProductById(int id);

        Task<ResponseInfo> AddProduct(AddProductDto addProductDto);

        Task<ResponseInfo> UpdateProduct(UpdateProductDto updateProductDto);

        Task<ResponseInfo> RemoveProductById(int id);

        Task<DataResponseInfo<List<GetProductDto>>> GetAllProductsByProductTypeId(int id);

        Task<DataResponseInfo<List<GetProductDto>>> GetAllProductsByOrderId(int id);
    }
}
