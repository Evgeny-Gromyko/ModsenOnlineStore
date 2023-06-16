using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;
using ModsenOnlineStore.Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Infrastructure.Services.OrderService
{
        public class OrderService : IOrderService
        {
            private IMapper mapper;
            private IOrderRepository repository;

            public OrderService(IMapper mapper, IOrderRepository repository)
            {
                this.mapper = mapper;
                this.repository = repository;
            }

            public async Task<ResponseInfo<List<GetOrderDTO>>> GetAllOrders()
            {
                var orders = await repository.GetAllOrders();
                var orderDTOs = orders.Select(p => mapper.Map<GetOrderDTO>(p)).ToList();
                if (orderDTOs.Count == 0)
                {
                    return new ResponseInfo<List<GetOrderDTO>>(orderDTOs, true, "no orders yet");
                }
                return new ResponseInfo<List<GetOrderDTO>>(orderDTOs, true, "all orders");
            }

            public async Task<ResponseInfo<GetOrderDTO>> GetSingleOrder(int id)
            {
                var order = await repository.GetSingleOrder(id);
                if (order is null)
                {
                    return new ResponseInfo<GetOrderDTO>(null, false, "order not found");
                }
                return new ResponseInfo<GetOrderDTO>(mapper.Map<GetOrderDTO>(order), true, $"order with id {id}");
            }

            public async Task<ResponseInfo<string>> AddOrder(AddOrderDTO addOrder)
            {
                var newOrder = mapper.Map<Order>(addOrder);
                await repository.AddOrder(newOrder);
                //return await GetAllOrders();
                return new ResponseInfo<string>(data: "added successfully", success: true, message: "order");
            }

            public async Task<ResponseInfo<string>> UpdateOrder(UpdateOrderDTO updateOrder)
            {
                var newOrder = mapper.Map<Order>(updateOrder);
                await repository.UpdateOrder(newOrder);
                //return await GetAllOrders();
                return new ResponseInfo<string>(data: "updated successfully", success: true, message: "order");
            }


            public async Task<ResponseInfo<GetOrderDTO>> PayOrder(int id, int userId)
            {

                var order = await repository.GetSingleOrder(id);
                if (order is null)
                {
                      return new ResponseInfo<GetOrderDTO>(mapper.Map<GetOrderDTO>(order), false, "order not found");
                }
                // return await GetAllOrders();
                return new ResponseInfo<GetOrderDTO>(mapper.Map<GetOrderDTO>(order), true, "order");
            }

            public async Task<ResponseInfo<string>> DeleteOrder(int id)
            {
                await repository.DeleteOrder(id);
                //return await GetAllOrders();
                return new ResponseInfo<string>(data: "deleted successfully", success: true, message: "order");
            }
        }

}
