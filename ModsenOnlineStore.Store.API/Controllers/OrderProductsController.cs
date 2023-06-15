using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs;
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
        public async Task<IActionResult> AddProductToOrder(AddOrderProductDTO data)
        {
            return Ok(await orderProductService.AddProductToOrder(data.ProductId, data.OrderId, data.Quantity));
        }
    }
}
