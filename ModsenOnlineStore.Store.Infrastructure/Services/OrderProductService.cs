using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Domain.DTOs;
using ModsenOnlineStore.Store.Infrastructure.Data;

namespace ModsenOnlineStore.Store.Infrastructure.Services;

public class OrderProductService : IOrderProductService
{
    private IMapper mapper;
    private IOrderProductRepository repository;

    public OrderProductService(IMapper mapper, IOrderProductRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<ResponseInfo<List<Domain.Entities.OrderProduct>>> GetAllOrderProducts()
    {
        var orderProducts = await repository.GetAllOrderProducts();
        return new ResponseInfo<List<Domain.Entities.OrderProduct>>(orderProducts, true, "all orders");
    }

    public async Task<ResponseInfo<GetOrderDTO>> AddProductToOrder(int productId, int orderId, int quantity = 1)
    {
        var order = await repository.AddProductToOrder(productId, orderId, quantity);

        if (order is null)
            return new ResponseInfo<GetOrderDTO>(null, false, "not found");
        else
        {
            return new ResponseInfo<GetOrderDTO>(mapper.Map<GetOrderDTO>(order), true, $"order with id {orderId}");
        }
    }
}