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
        Task<ResponseInfo<string>> AddOrder(AddOrderDTO addOrder);
        Task<ResponseInfo<string>> UpdateOrder(UpdateOrderDTO updateOrder);
        Task<ResponseInfo<string>> DeleteOrder(int id);
        Task<ResponseInfo<GetOrderDTO>> PayOrder(int id, int userId);
    }
}
