using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;

public interface IOrderProductRepository
{
    public Task<Order?> AddProductToOrderAsync(int productId, int orderId, int quantity = 1);

    public Task<List<OrderProduct>> GetAllOrderProductsAsync();
}
