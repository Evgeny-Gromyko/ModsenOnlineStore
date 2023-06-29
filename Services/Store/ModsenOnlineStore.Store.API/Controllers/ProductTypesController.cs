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
        public async Task<IActionResult> GetAllProductTypes()
        {
            var response = await productTypeService.GetAllProductTypes();
                
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleProductType(int id)
        {
            var response = await productTypeService.GetSingleProductType(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProductType(AddUpdateProductTypeDTO product)
        {
            var response = await productTypeService.AddProductType(product);
                
            return Ok(response.Message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProductType(int id, AddUpdateProductTypeDTO newProduct)
        {
            var response = await productTypeService.UpdateProductType(id, newProduct);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await productTypeService.DeleteProductType(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }
    }
}
