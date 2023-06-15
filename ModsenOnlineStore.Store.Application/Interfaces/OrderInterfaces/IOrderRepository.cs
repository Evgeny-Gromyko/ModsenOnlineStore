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
        Task<List<Order>> AddOrder(Order order);///////
        Task<List<Order>> UpdateOrder(int id, Order order);///////
        Task<List<Order>> DeleteOrder(int id);
        Task<List<Order>> PayOrder(int id, int userId);
    }
}
