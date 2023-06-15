using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data;

public class OrderProductRepository:IOrderProductRepository
{
    private DataContext context;
    
    public OrderProductRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<Order?> AddProductToOrder(int productId, int orderId, int quantity = 1)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        
        if (product is null)
            return null;
        
        var order = await context.Orders.
            Include(o => o.User).
            FirstOrDefaultAsync(p => p.Id == orderId);
        
        if (order is null)
            return null;
                
        var orderProduct = await context.OrderProducts.FirstOrDefaultAsync(op => op.ProductId == productId && op.OrderId == orderId);
        
        if (orderProduct is null)
        {
            var newOrderProduct = new OrderProduct() { Product = product, Order = order, ProductQuantity = quantity };
            context.OrderProducts.Add(newOrderProduct);

        }
        else 
            orderProduct.ProductQuantity += quantity;

        order.TotalPrice += product.Price * quantity;
        await context.SaveChangesAsync();
        
        return order;
    }

    public async Task<List<OrderProduct>> GetAllOrderProducts()
    {
        var orderProducts = await context.OrderProducts
            .Include(op => op.Order)
            .Include(op => op.Product)
            .ToListAsync();

        return orderProducts;
    }
}