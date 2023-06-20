using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;
using ModsenOnlineStore.Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
