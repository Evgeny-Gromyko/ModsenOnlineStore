using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces
{
    public interface IOrderService
    {
        Task<DataResponseInfo<List<GetOrderDTO>>> GetAllOrders();

        Task<DataResponseInfo<GetOrderDTO>> GetSingleOrder(int id);

        Task<ResponseInfo> AddOrder(AddOrderDTO addOrder);

        Task<ResponseInfo> UpdateOrder(UpdateOrderDTO updateOrder);

        Task<ResponseInfo> PayOrder(int id, string code);

        Task<ResponseInfo> DeleteOrder(int id);

        Task<DataResponseInfo<List<GetOrderDTO>>> GetAllOrdersByUserId(int id);
    }
}
