using Microsoft.AspNetCore.Http;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces
{
    public interface IOrderService
    {
        Task<DataResponseInfo<List<GetOrderDTO>>> GetAllOrders();

        Task<DataResponseInfo<GetOrderDTO>> GetSingleOrder(int id);

        Task<ResponseInfo> AddOrderAsync(AddOrderDTO addOrder);

        Task<ResponseInfo> UpdateOrderAsync(UpdateOrderDTO updateOrder);

        Task<ResponseInfo> PayOrderAsync(int id, string confirmationEmail, HttpRequest httpRequest);

        Task<ResponseInfo> ConfirmOrderPaymentAsync(int id, string confirmationEmail, string code);

        Task<ResponseInfo> DeleteOrderAsync(int id);

        Task<DataResponseInfo<List<GetOrderDTO>>> GetAllOrdersByUserIdAsync(int id);
    }
}
