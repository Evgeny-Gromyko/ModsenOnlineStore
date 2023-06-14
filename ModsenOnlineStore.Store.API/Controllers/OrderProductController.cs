using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Domain.DTOs;

namespace ModsenOnlineStore.Store.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private IOrderProductService orderProductService;

        public OrderProductController(IOrderProductService orderProductService)
        {
            this.orderProductService = orderProductService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            // Console.WriteLine(HttpContext.Request.Cookies);
            return Ok(await orderProductService.GetAllOrderProducts());
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToOrder(AddOrderProductDTO data)
        {
            return Ok(await orderProductService.AddProductToOrder(data.ProductId, data.OrderId, data.Quantity));
        }
    }
}
