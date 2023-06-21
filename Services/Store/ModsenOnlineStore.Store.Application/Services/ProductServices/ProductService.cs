using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Services.ProductServices
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

        public async Task<DataResponseInfo<List<GetProductDto>>> GetAllProducts()
        {
            var products = await repository.GetAllProducts();
            var productDtos = products.Select(mapper.Map<GetProductDto>).ToList();

            return new DataResponseInfo<List<GetProductDto>>(data: productDtos, success: true, message: "all products");
        }

        public async Task<DataResponseInfo<GetProductDto>> GetProductById(int id)
        {
            var product = await repository.GetProductById(id);

            if (product is null)
            {
                return new DataResponseInfo<GetProductDto>(data: null, success: true, message: "product");
            }

            var productDto = mapper.Map<GetProductDto>(product);

            return new DataResponseInfo<GetProductDto>(data: productDto, success: true, message: "product");
        }

        public async Task<ResponseInfo> AddProduct(AddProductDto addProductDto)
        {
            var product = mapper.Map<Product>(addProductDto);
            await repository.AddProduct(product);

            return new ResponseInfo(success: true, message: "product added");
        }

        public async Task<ResponseInfo> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var product = mapper.Map<Product>(updateProductDto);
            await repository.UpdateProduct(product);

            return new ResponseInfo(success: true, message: "product updated");
        }

        public async Task<ResponseInfo> RemoveProductById(int id)
        {
            await repository.RemoveProductById(id);

            return new ResponseInfo(success: true, message: "product removed");
        }
    }
}
