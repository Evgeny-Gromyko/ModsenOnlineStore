using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;

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
        public async Task<IActionResult> GetAllProductTypes()
        {
            return Ok(await productTypeService.GetAllProductTypes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleProductType(int id)
        {
            var productTypeInfo = await productTypeService.GetSingleProductType(id);
            
            if (!productTypeInfo.Success)
            {
                return NotFound();
            }
            
            return Ok(productTypeInfo);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductType(AddUpdateProductTypeDTO product)
        {
            return Ok(await productTypeService.AddProductType(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductType(int id, AddUpdateProductTypeDTO newProduct)
        {
            var productRes = await productTypeService.UpdateProductType(id, newProduct);
           
            if (!productRes.Success)
            {
                return NotFound();
            }
            
            return Ok(productRes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await productTypeService.DeleteProductType(id);
           
            if (!result.Success)
            {
                return NotFound();
            }
            
            return Ok(result);
        }
    }
}
