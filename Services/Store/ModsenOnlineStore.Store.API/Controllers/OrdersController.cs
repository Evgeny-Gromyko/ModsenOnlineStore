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
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await orderService.GetAllOrders();
            
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSingleOrder(int id)
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
        public async Task<IActionResult> AddOrder(AddOrderDTO order)
        {
            var response = await orderService.AddOrder(order);
                
            return Ok(response.Message);
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDTO order)
        {
            var response = await orderService.UpdateOrder(order);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await orderService.DeleteOrder(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("byUser{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllOrdersByUserId(int id)
        {
            var response = await orderService.GetAllOrdersByUserId(id);
                
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            
            return Ok(response.Data);
        }
    }
}
