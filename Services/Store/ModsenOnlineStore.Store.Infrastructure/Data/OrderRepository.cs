using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext context;

        public OrderRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await context.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Order?> GetSingleOrder(int id)
        {
            return await context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddOrder(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order is not null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
        }
    }
}
