using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;
using ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;

namespace ModsenOnlineStore.Store.Application.Services.OrderProductServices;

public class OrderProductService : IOrderProductService
{
    private readonly IMapper mapper;
    private readonly IOrderProductRepository orderProductRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IProductRepository productRepository;

    public OrderProductService(IMapper mapper, IOrderProductRepository orderProductRepository, IOrderRepository orderRepository, IProductRepository productRepository)
    {
        this.mapper = mapper;
        this.orderProductRepository = orderProductRepository;
        this.orderRepository = orderRepository;
        this.productRepository = productRepository;
    }

    public async Task<DataResponseInfo<List<GetOrderProductDTO>>> GetAllOrderProductsAsync()
    {
        var orderProducts = await orderProductRepository.GetAllOrderProductsAsync();
        var orderProductDtos = orderProducts.Select(mapper.Map<GetOrderProductDTO>).ToList();

        return new DataResponseInfo<List<GetOrderProductDTO>>(data: orderProductDtos, success: true, message: "all orders");
    }

    public async Task<ResponseInfo> AddProductToOrderAsync(AddProductToOrderDTO addProductToOrderDto)
    {
        var productId = addProductToOrderDto.ProductId;
        var orderId = addProductToOrderDto.OrderId;
        var quantity = addProductToOrderDto.Quantity;

        var order = await orderRepository.GetSingleOrderAsync(addProductToOrderDto.OrderId);

        if (order is null)
        {
            return new ResponseInfo(success: false, message: "no such order");
        }

        var product = await productRepository.GetProductByIdAsync(addProductToOrderDto.ProductId);

        if (product is null)
        {
            return new ResponseInfo(success: false, message: "no such product");
        }

        if (product.Quantity < quantity)
        {
            return new ResponseInfo(success: false, message: "product quantity greater than available");
        }

        product.Quantity -= quantity;
        await productRepository.UpdateProductAsync(product);
        await orderProductRepository.AddProductToOrderAsync(productId, orderId, quantity);

        return new ResponseInfo(success: true, message: $"order with id {orderId} added");
    }
}
