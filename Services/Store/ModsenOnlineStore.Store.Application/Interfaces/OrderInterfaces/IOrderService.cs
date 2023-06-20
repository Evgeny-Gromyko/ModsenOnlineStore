using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces
{
    public interface IOrderService
    {
        Task<ResponseInfo> GetAllOrders();

        Task<ResponseInfo> GetSingleOrder(int id);

        Task<ResponseInfo> AddOrder(AddOrderDTO addOrder);

        Task<ResponseInfo> UpdateOrder(UpdateOrderDTO updateOrder);

        Task<ResponseInfo> DeleteOrder(int id);
    }
}
