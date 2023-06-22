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
            return Ok(await productTypeService.GetSingleProductType(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddProductType(AddUpdateProductTypeDTO product)
        {
            return Ok(await productTypeService.AddProductType(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductType(int id, AddUpdateProductTypeDTO newProduct)
        {
            return Ok(await productTypeService.UpdateProductType(id, newProduct));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await productTypeService.DeleteProductType(id));
        }
    }
}
