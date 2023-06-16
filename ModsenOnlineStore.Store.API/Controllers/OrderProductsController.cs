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
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await orderProductService.GetAllOrderProducts());
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToOrder(AddProductToOrderDTO data)
        {
            return Ok(await orderProductService.AddProductToOrder(data));
        }
    }
}
