using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces;

public interface IOrderProductRepository
{
    public Task<Order?> AddProductToOrder(int productId, int orderId, int quantity = 1);

    public Task<List<OrderProduct>> GetAllOrderProducts();
}