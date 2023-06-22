using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;
using ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces;
using ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductTypeRepository productTypeRepository;
        private readonly IOrderProductRepository orderProductRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IOrderProductRepository orderProductRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productTypeRepository = productTypeRepository;
            this.orderProductRepository = orderProductRepository;
            this.mapper = mapper;
        }

        public async Task<DataResponseInfo<List<GetProductDto>>> GetAllProducts()
        {
            var products = await productRepository.GetAllProducts();
            var productDtos = products.Select(mapper.Map<GetProductDto>).ToList();

            return new DataResponseInfo<List<GetProductDto>>(data: productDtos, success: true, message: "all products");
        }

        public async Task<DataResponseInfo<GetProductDto>> GetProductById(int id)
        {
            var product = await productRepository.GetProductById(id);

            if (product is null)
            {
                return new DataResponseInfo<GetProductDto>(data: null, success: false, message: "no such product");
            }

            var productDto = mapper.Map<GetProductDto>(product);

            return new DataResponseInfo<GetProductDto>(data: productDto, success: true, message: "product");
        }

        public async Task<ResponseInfo> AddProduct(AddProductDto addProductDto)
        {
            var productType = await productTypeRepository.GetSingleProductType(addProductDto.ProductTypeId);

            if (productType is null)
            {
                return new ResponseInfo(success: false, message: "no such product type");
            }

            var product = mapper.Map<Product>(addProductDto);
            await productRepository.AddProduct(product);

            return new ResponseInfo(success: true, message: "product added");
        }

        public async Task<ResponseInfo> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var oldProduct = await productRepository.GetProductById(updateProductDto.Id);

            if (oldProduct is null)
            {
                return new ResponseInfo(success: false, message: "no such product");
            }

            var productType = await productTypeRepository.GetSingleProductType(updateProductDto.ProductTypeId);

            if (productType is null)
            {
                return new ResponseInfo(success: false, message: "no such product type");
            }

            var product = mapper.Map<Product>(updateProductDto);
            await productRepository.UpdateProduct(product);

            return new ResponseInfo(success: true, message: "product updated");
        }

        public async Task<ResponseInfo> RemoveProductById(int id)
        {
            var product = await productRepository.GetProductById(id);

            if (product is null)
            {
                return new ResponseInfo(success: false, message: "no such product");
            }

            await productRepository.RemoveProductById(id);

            return new ResponseInfo(success: true, message: "product removed");
        }

        public async Task<DataResponseInfo<List<GetProductDto>>> GetAllProductsByProductTypeId(int id)
        {
            var products = await productRepository.GetAllProducts();
            var productTypeProducts = products.FindAll(p => p.ProductTypeId == id);
            var productDtos = productTypeProducts.Select(mapper.Map<GetProductDto>).ToList();

            return new DataResponseInfo<List<GetProductDto>>(data: productDtos, success: true, message: "all products of product type");
        }

        public async Task<DataResponseInfo<List<GetProductDto>>> GetAllProductsByOrderId(int id)
        {
            var allOrderProducts = await orderProductRepository.GetAllOrderProducts();
            var orderProducts = allOrderProducts.FindAll(o => o.OrderId == id);

            var productDtos = new List<GetProductDto>();

            foreach (var orderProduct in orderProducts)
            {
                var product = await productRepository.GetProductById(orderProduct.ProductId);
                var productDto = mapper.Map<GetProductDto>(product);
                productDtos.Add(productDto);
            }

            return new DataResponseInfo<List<GetProductDto>>(data: productDtos, success: true, message: "all products of order");
        }
    }
}
