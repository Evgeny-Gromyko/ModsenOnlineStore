using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;

namespace ModsenOnlineStore.Store.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService)
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
            return Ok(await orderService.GetSingleOrder(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderDTO order)
        {
            return Ok(await orderService.AddOrder(order));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDTO order)
        {
            return Ok(await orderService.UpdateOrder(order));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(await orderService.DeleteOrder(id));
        }
    }
}
