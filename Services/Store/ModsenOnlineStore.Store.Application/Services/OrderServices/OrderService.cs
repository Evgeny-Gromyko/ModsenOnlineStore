using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;
using ModsenOnlineStore.Store.Domain.Entities;
using System.Net.Http.Json;

namespace ModsenOnlineStore.Store.Application.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;

        public OrderService(IMapper mapper, IOrderRepository repository)
        {
            this.mapper = mapper;
            this.orderRepository = repository;
        }

        public async Task<DataResponseInfo<List<GetOrderDTO>>> GetAllOrders()
        {
            var orders = await orderRepository.GetAllOrders();
            var orderDTOs = orders.Select(p => mapper.Map<GetOrderDTO>(p)).ToList();

            return new DataResponseInfo<List<GetOrderDTO>>(data: orderDTOs, success: true, message: "all orders");
        }

        public async Task<DataResponseInfo<GetOrderDTO>> GetSingleOrder(int id)
        {
            var order = await orderRepository.GetSingleOrderAsync(id);

            if (order is null)
            {
                return new DataResponseInfo<GetOrderDTO>(data: null, success: false, message: "no such order");
            }

            return new DataResponseInfo<GetOrderDTO>(data: mapper.Map<GetOrderDTO>(order), success: true, message: "order");
        }

        public async Task<ResponseInfo> AddOrderAsync(AddOrderDTO addOrder)
        {
            string confirmationCode;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7107/");
                var response = await client.PostAsJsonAsync("EmaiLoginlAuthentication", "egrom2002@gmail.com");
                if (!response.IsSuccessStatusCode)
                {
                    return new ResponseInfo(success: false, message: "can not get confirmation code");
                }
                
                confirmationCode = await response.Content.ReadAsStringAsync();
            }

            var newOrder = mapper.Map<Order>(addOrder);

            newOrder.PaymentConfirmationCode = confirmationCode;

            await orderRepository.AddOrderAsync(newOrder);

            return new ResponseInfo(success: true, message: "order added");
        }

        public async Task<ResponseInfo> UpdateOrderAsync(UpdateOrderDTO updateOrder)
        {
            var oldOrder = await orderRepository.GetSingleOrderAsync(updateOrder.Id);

            if (oldOrder is null)
            {
                return new ResponseInfo(success: false, message: "no such order");
            }

            var newOrder = mapper.Map<Order>(updateOrder);
            await orderRepository.UpdateOrderAsync(newOrder);

            return new ResponseInfo(success: true, message: "order updated");
        }


        public async Task<ResponseInfo> PayOrderAsync(int id, string code)
        {
            var order = await orderRepository.GetSingleOrderAsync(id);

            if (order is null)
            {
                return new ResponseInfo(success: false, message: "no such order");
            }

            if (order.PaymentConfirmationCode != code)
            {
                return new ResponseInfo(success: false, message: "wrong confirmation code");
            }

            using (var client = new HttpClient())  // request to reduce money
            {
                client.BaseAddress = new Uri("https://localhost:7123/");
                var response = await client.PostAsJsonAsync($"Login/Pay/{order.UserId}", order.TotalPrice);

                if (response.IsSuccessStatusCode) {
                    order.Paid = true;
                    await orderRepository.UpdateOrderAsync(order);
                }

                return await response.Content.ReadFromJsonAsync<ResponseInfo>(); //Paid or not
            }
        }

        public async Task<ResponseInfo> DeleteOrderAsync(int id)
        {
            var order = await orderRepository.GetSingleOrderAsync(id);

            if (order is null)
            {
                return new ResponseInfo(success: false, message: "no such order");
            }

            await orderRepository.DeleteOrderAsync(id);

            return new ResponseInfo(success: true, message: "order deleted");
        }

        public async Task<DataResponseInfo<List<GetOrderDTO>>> GetAllOrdersByUserIdAsync(int id)
        {
            var orders = await orderRepository.GetAllOrders();
            var userOrders = orders.FindAll(o => o.UserId == id);
            var orderDTOs = userOrders.Select(mapper.Map<GetOrderDTO>).ToList();

            return new DataResponseInfo<List<GetOrderDTO>>(data: orderDTOs, success: true, message: "all orders");
        }
    }
}
