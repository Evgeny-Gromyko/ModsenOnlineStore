using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetSingleOrder(int id);
    
    Task UpdateOrder(Order order);
}