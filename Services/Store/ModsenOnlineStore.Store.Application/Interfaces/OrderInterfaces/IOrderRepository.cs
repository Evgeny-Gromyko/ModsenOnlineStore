using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();

        Task<Order?> GetSingleOrderAsync(int id);

        Task AddOrderAsync(Order order);

        Task UpdateOrderAsync(Order order);

        Task DeleteOrderAsync(int id);
    }
}
