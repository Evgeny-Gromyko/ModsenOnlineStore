using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;

namespace ModsenOnlineStore.Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleOrder(int id)
        {
            var response = await orderService.GetSingleOrder(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderDTO order)
        {
            return Ok(await orderService.AddOrder(order));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDTO order)
        {
            var response = await orderService.UpdateOrder(order);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await orderService.DeleteOrder(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("byUser{id}")]
        public async Task<IActionResult> GetAllOrdersByUserId(int id)
        {
            return Ok(await orderService.GetAllOrdersByUserId(id));
        }
    }
}
