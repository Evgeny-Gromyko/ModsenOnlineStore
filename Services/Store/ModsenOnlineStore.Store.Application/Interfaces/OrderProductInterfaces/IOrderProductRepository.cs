using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;

public interface IOrderProductRepository
{
    public Task<Order?> AddProductToOrderAsync(int productId, int orderId, int quantity = 1);

    public Task<List<OrderProduct>> GetAllOrderProductsAsync(int pageNumber, int pageSize);

    public Task<List<OrderProduct>> GetAllOrderProductsByOrderIdAsync(int id, int pageNumber, int pageSize);
}
