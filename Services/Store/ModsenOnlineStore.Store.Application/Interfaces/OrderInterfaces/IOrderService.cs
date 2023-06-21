using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces
{
    public interface IOrderService
    {
        Task<DataResponseInfo<List<GetOrderDTO>>> GetAllOrders();

        Task<DataResponseInfo<GetOrderDTO>> GetSingleOrder(int id);

        Task<ResponseInfo> AddOrder(AddOrderDTO addOrder);

        Task<ResponseInfo> UpdateOrder(UpdateOrderDTO updateOrder);

        Task<ResponseInfo> DeleteOrder(int id);
    }
}
