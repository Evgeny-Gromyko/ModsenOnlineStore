using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;

public interface IOrderProductService
{
    public Task<OperationResult> AddProductToOrder(AddProductToOrderDTO dto);
    
    public Task<ResponseInfo<List<GetOrderProductDTO>>> GetAllOrderProducts();
}