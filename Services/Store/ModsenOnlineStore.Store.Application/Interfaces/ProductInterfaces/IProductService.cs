using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces
{
    public interface IProductService
    {
        Task<ResponseInfo> GetAllProducts();

        Task<ResponseInfo> GetProductById(int id);

        Task<ResponseInfo> AddProduct(AddProductDto addProductDto);

        Task<ResponseInfo> UpdateProduct(UpdateProductDto updateProductDto);

        Task<ResponseInfo> RemoveProductById(int id);
    }
}
