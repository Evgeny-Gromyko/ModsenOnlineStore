using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces
{
    public interface IProductService
    {
        Task<ResponseInfo<List<GetProductDto>>> GetAllProducts();
        Task<ResponseInfo<GetProductDto>> GetProductById(int id);
        Task<ResponseInfo<List<GetProductDto>>> AddProduct(AddProductDto addProductDto);
        Task<ResponseInfo<List<GetProductDto>>> UpdateProduct(UpdateProductDto updateProductDto);
        Task<ResponseInfo<List<GetProductDto>>> RemoveProductById(int id);
    }
}
