using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs;


namespace ModsenOnlineStore.Store.Application.Interfaces;

public interface IOrderProductService
{
    public Task<ResponseInfo<GetOrderDTO>> AddProductToOrder(int productId, int orderId, int quantity = 1);
    public Task<ResponseInfo<List<Domain.Entities.OrderProduct>>> GetAllOrderProducts();
}