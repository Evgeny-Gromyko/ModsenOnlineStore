using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;

namespace ModsenOnlineStore.Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService service;

        public ProductsController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var response = await service.GetAllProductsAsync();

            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            throw new Exception("my ex");
            var response = await service.GetProductByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProductAsync(AddProductDTO addProductDto)
        {
            var response = await service.AddProductAsync(addProductDto);

            return Ok(response.Message);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProductAsync(UpdateProductDTO updateProductDto)
        {
            var response = await service.UpdateProductAsync(updateProductDto);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveProductByIdAsync(int id)
        {
            var response = await service.RemoveProductByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("byProductType{id}")]
        public async Task<IActionResult> GetAllProductsByProductTypeIdAsync(int id)
        {
            var response = await service.GetAllProductsByProductTypeIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpGet("byOrder{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllProductsByOrderIdAsync(int id)
        {
            var response = await service.GetAllProductsByOrderIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
