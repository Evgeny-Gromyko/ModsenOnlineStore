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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private IProductTypeService productTypeService;

        public ProductTypesController(IProductTypeService productTypeService)
        {
            this.productTypeService = productTypeService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await productTypeService.GetAllProductTypes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleProduct(int id)
        {
            var product = await productTypeService.GetSingleProductType(id);
            
            if (product is null)
            {
                return NotFound("no such product found");
            }
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddUpdateProductTypeDTO product)
        {
            return Ok(await productTypeService.AddProductType(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, AddUpdateProductTypeDTO newProduct)
        {
            var product = await productTypeService.UpdateProductType(id, newProduct);
           
            if (product is null)
            {
                return NotFound("no such product found");
            }
            
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await productTypeService.DeleteProductType(id);
           
            if (result is null)
            {
                return NotFound("no such product found");
            }
            
            return Ok(result);
        }
    }
}
