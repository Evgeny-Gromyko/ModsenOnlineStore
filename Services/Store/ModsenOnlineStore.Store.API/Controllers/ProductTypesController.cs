using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;

namespace ModsenOnlineStore.Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypeService productTypeService;

        public ProductTypesController(IProductTypeService productTypeService)
        {
            this.productTypeService = productTypeService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllProductTypesAsync()
        {
            var response = await productTypeService.GetAllProductTypesAsync();
                
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleProductTypeAsync(int id)
        {
            var response = await productTypeService.GetSingleProductTypeAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProductTypeAsync(AddUpdateProductTypeDTO product)
        {
            var response = await productTypeService.AddProductTypeAsync(product);
                
            return Ok(response.Message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProductTypeAsync(int id, AddUpdateProductTypeDTO newProduct)
        {
            var response = await productTypeService.UpdateProductTypeAsync(id, newProduct);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var response = await productTypeService.DeleteProductTypeAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }
    }
}
