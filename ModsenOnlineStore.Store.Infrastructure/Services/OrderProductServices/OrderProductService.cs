using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;

namespace ModsenOnlineStore.Store.Infrastructure.Services.OrderProductServices;

public class OrderProductService : IOrderProductService
{
    private IMapper mapper;
    
    private IOrderProductRepository repository;

    public OrderProductService(IMapper mapper, IOrderProductRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<ResponseInfo<List<GetOrderProductDTO>>> GetAllOrderProducts()
    {
        var orderProducts = await repository.GetAllOrderProducts();
        var orderProductDtos = orderProducts.Select(mapper.Map<GetOrderProductDTO>).ToList();

        return new ResponseInfo<List<GetOrderProductDTO>>(orderProductDtos, true, "all orders");
    }

    public async Task<ResponseInfo<string>> AddProductToOrder(int productId, int orderId, int quantity = 1)
    {
        var order = await repository.AddProductToOrder(productId, orderId, quantity);

        if (order is null)
            return new ResponseInfo<string>(null, false, "not found");
        else
        {
            return new ResponseInfo<string>("added successfully", true, $"order with id {orderId}");
        }
    }
}