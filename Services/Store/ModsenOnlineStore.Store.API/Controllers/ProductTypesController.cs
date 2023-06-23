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
            return Ok(await productTypeService.GetAllProductTypes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleProductType(int id)
        {
            var response = await productTypeService.GetSingleProductType(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductType(AddUpdateProductTypeDTO product)
        {
            return Ok(await productTypeService.AddProductType(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductType(int id, AddUpdateProductTypeDTO newProduct)
        {
            var response = await productTypeService.UpdateProductType(id, newProduct);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await productTypeService.DeleteProductType(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
