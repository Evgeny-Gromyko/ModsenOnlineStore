using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseInfo<List<GetProductDto>>> GetAllProducts()
        {
            var products = await repository.GetAllProducts();
            var productDtos = products.Select(mapper.Map<GetProductDto>).ToList();

            return new ResponseInfo<List<GetProductDto>>(data: productDtos, success: true, message: "all products");
        }

        public async Task<ResponseInfo<GetProductDto>> GetProductById(int id)
        {
            var product = await repository.GetProductById(id);

            if (product is null)
            {
                return new ResponseInfo<GetProductDto>(data: null, success: true, message: "product");
            }

            var productDto = mapper.Map<GetProductDto>(product);

            return new ResponseInfo<GetProductDto>(productDto, true, "product");
        }

        public async Task<ResponseInfo<string>> AddProduct(AddProductDto addProductDto)
        {
            var product = mapper.Map<Product>(addProductDto);
            await repository.AddProduct(product);

            return new ResponseInfo<string>(data: "added successfully", success: true, message: "product");
        }

        public async Task<ResponseInfo<string>> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var product = mapper.Map<Product>(updateProductDto);
            await repository.UpdateProduct(product);

            return new ResponseInfo<string>(data: "updated successfully", success: true, message: "product");
        }

        public async Task<ResponseInfo<string>> RemoveProductById(int id)
        {
            await repository.RemoveProductById(id);

            return new ResponseInfo<string>(data: "removed successfully", success: true, message: "product");
        }
    }
}
