using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ModsenOnlineStore.Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IConfiguration configuration;

        public OrdersController(IOrderService orderService, IConfiguration configuration)
        {
            this.configuration = configuration; 
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
        public async Task<IActionResult> AddOrder(AddOrderDTO order) // send mail, save code to database
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7107/");
                var response = await client.PostAsJsonAsync("EmaiLoginlAuthentication", "egrom2002@gmail.com");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    order.PaymentConfirmationCode = data;
                }
            }

            return Ok(await orderService.AddOrder(order));
        }

        [HttpPut("Pay")]
        public async Task<IActionResult> PayOrder(int id, string code)
        {
            var response = await orderService.PayOrder(id, code);

            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
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
