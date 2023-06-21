using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();

        Task<Order?> GetSingleOrder(int id);

        Task AddOrder(Order order);

        Task UpdateOrder(Order order);

        Task DeleteOrder(int id);
    }
}
