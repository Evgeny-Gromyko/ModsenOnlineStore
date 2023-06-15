using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces
{
    public interface IOrderService
    {
        Task<ResponseInfo<List<GetOrderDTO>>> GetAllOrders();
        Task<ResponseInfo<GetOrderDTO>> GetSingleOrder(int id);
        Task<ResponseInfo<List<GetOrderDTO>>> AddOrder(AddOrderDTO order);
        Task<ResponseInfo<List<GetOrderDTO>>> UpdateOrder(int id, UpdateOrderDTO order);
        Task<ResponseInfo<List<GetOrderDTO>>> DeleteOrder(int id);
        Task<ResponseInfo<List<GetOrderDTO>>> PayOrder(int id, int userId);
    }
}
