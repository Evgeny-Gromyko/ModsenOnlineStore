using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data
{
    public class OrderRepository : PagedRepository<Order>, IOrderRepository
    {
        private readonly DataContext context;

        public OrderRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Order>> GetAllOrders(int pageNumber, int pageSize)
        {
            var orders = await context.Orders.AsNoTracking().ToListAsync();

            return ToPagedList(orders, pageNumber, pageSize);
        }

        public async Task<Order?> GetSingleOrderAsync(int id)
        {
            return await context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order is not null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetAllOrdersByUserId(int id, int pageNumber, int pageSize)
        {
            var orders = await context.Orders.AsNoTracking().Where(p => p.UserId == id).ToListAsync();

            return ToPagedList(orders, pageNumber, pageSize);
        }
    }
}
