using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Domain.DTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Services
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
            return new ResponseInfo<List<GetProductDto>>(productDtos, true, "all products");
        }

        public async Task<ResponseInfo<GetProductDto>> GetProductById(int id)
        {
            var product = await repository.GetProductById(id);

            if (product is null)
            {
                return new ResponseInfo<GetProductDto>(null, true, "product");
            }

            var productDto = mapper.Map<GetProductDto>(product);
            return new ResponseInfo<GetProductDto>(productDto, true, "product");
        }

        public async Task<ResponseInfo<List<GetProductDto>>> AddProduct(AddProductDto putProductDto)
        {
            var product = mapper.Map<Product>(putProductDto);
            await repository.AddProduct(product);
            return await GetAllProducts();
        }

        public async Task<ResponseInfo<List<GetProductDto>>> UpdateProduct(UpdateProductDto putProductDto)
        {
            var product = mapper.Map<Product>(putProductDto);
            await repository.UpdateProduct(product);
            return await GetAllProducts();
        }

        public async Task<ResponseInfo<List<GetProductDto>>> RemoveProductById(int id)
        {
            await repository.RemoveProductById(id);
            return await GetAllProducts();
        }
    }
}
