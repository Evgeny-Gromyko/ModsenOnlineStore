using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;

namespace ModsenOnlineStore.Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductsController : ControllerBase
    {
        private IOrderProductService orderProductService;

        public OrderProductsController(IOrderProductService orderProductService)
        {
            this.orderProductService = orderProductService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrderProductsAsync()
        {
            var response = await orderProductService.GetAllOrderProductsAsync();
            
            return Ok(response.Data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProductToOrderAsync(AddProductToOrderDTO data)
        {
            var response = await orderProductService.AddProductToOrderAsync(data);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }
    }
}
