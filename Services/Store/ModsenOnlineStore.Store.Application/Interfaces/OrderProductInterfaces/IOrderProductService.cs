using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;

public interface IOrderProductService
{
    public Task<ResponseInfo> AddProductToOrderAsync(AddProductToOrderDTO dto);
    
    public Task<DataResponseInfo<List<GetOrderProductDTO>>> GetAllOrderProductsAsync();
}