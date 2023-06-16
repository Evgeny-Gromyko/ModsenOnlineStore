using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;
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


        public async Task AddOrder(Order order)///////
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            //return await GetAllOrders();
        }

        public async Task UpdateOrder(Order order)///////
        {
            //var order = await context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
            context.Orders.Update(order);
            await context.SaveChangesAsync();
            //return await GetAllOrders();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order is not null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }

            //return await GetAllOrders();
        }

        public async Task PayOrder(int id, int userId)

        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order is not null)
            {
                order.Paid = true;
                await context.SaveChangesAsync();
            }

            ////////////////
            /*var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                return new ResponseInfo<List<GetOrderDTO>>(null, false, "user not found");
            }

            if (user.Money < order.TotalPrice)
            {
                new ResponseInfo<GetOrderDTO>(null, false, "not enough money");
            }
            user.Money -= order.TotalPrice;
            //order.Paid = true;
            await repository.SaveChangesAsync();*/

            //return await GetAllOrders();
        }

    }    
}
