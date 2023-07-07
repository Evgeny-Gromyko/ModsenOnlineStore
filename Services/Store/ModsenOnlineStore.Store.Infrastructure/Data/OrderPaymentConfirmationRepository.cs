using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces.OrderPaymentConfirmationInterfaces;
using ModsenOnlineStore.Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Infrastructure.Data
{
    public class OrderPaymentConfirmationRepository : IOrderPaymentConfirmationRepository
    {
        private readonly DataContext context;

        public OrderPaymentConfirmationRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<OrderPaymentConfirmation?> GetOrderPaymentConfirmationAsync(int orderId, string code)
        {
            var orderPaymentConfirmations = context.OrderPaymentConfirmations.AsNoTracking();
            var orderPaymentConfirmation = await orderPaymentConfirmations.FirstOrDefaultAsync(ec => ec.OrderId == orderId && ec.Code == code);

            return orderPaymentConfirmation;
        }

        public async Task AddOrderPaymentConfirmationAsync(OrderPaymentConfirmation emailConfirmation)
        {
            context.OrderPaymentConfirmations.Add(emailConfirmation);
            await context.SaveChangesAsync();
        }

        public async Task RemoveOrderPaymentConfirmationByIdAsync(int id)
        {
            var emailConfirmations = context.OrderPaymentConfirmations.AsNoTracking();
            var emailConfirmation = await emailConfirmations.FirstOrDefaultAsync(ec => ec.Id == id);

            if (emailConfirmation is not null)
            {
                context.OrderPaymentConfirmations.Remove(emailConfirmation);
                await context.SaveChangesAsync();
            }
        }
    }
}
