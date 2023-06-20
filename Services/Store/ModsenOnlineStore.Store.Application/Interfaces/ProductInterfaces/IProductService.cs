using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces
{
    public interface IProductService
    {
        Task<ResponseInfo<List<GetProductDto>>> GetAllProducts();

        Task<ResponseInfo<GetProductDto>> GetProductById(int id);

        Task<ResponseInfo<string>> AddProduct(AddProductDto addProductDto);

        Task<ResponseInfo<string>> UpdateProduct(UpdateProductDto updateProductDto);

        Task<ResponseInfo<string>> RemoveProductById(int id);
    }
}
