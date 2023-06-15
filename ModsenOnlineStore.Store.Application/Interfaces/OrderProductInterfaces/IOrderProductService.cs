using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;

public interface IOrderProductService
{
    public Task<ResponseInfo<string>> AddProductToOrder(int productId, int orderId, int quantity = 1);
    
    public Task<ResponseInfo<List<GetOrderProductDTO>>> GetAllOrderProducts();
}