using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var response = await orderService.GetAllOrders();
            
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSingleOrderAsync(int id)
        {
            var response = await orderService.GetSingleOrder(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddOrderAsync(AddOrderDTO order)
        {
            var response = await orderService.AddOrderAsync(order);
                
            return Ok(response.Message);
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateOrderAsync(UpdateOrderDTO order)
        {
            var response = await orderService.UpdateOrderAsync(order);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var response = await orderService.DeleteOrderAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("byUser{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllOrdersByUserIdAsync(int id)
        {
            var response = await orderService.GetAllOrdersByUserIdAsync(id);
                
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            
            return Ok(response.Data);
        }
    }
}
