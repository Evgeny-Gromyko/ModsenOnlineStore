using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext context;

    public OrderRepository(DataContext context)
    {
        this.context = context;
    }
    
    public async Task UpdateOrder(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();
    }
    
    public async Task<Order?> GetSingleOrder(int id)
    {
        return await context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
    }
}
