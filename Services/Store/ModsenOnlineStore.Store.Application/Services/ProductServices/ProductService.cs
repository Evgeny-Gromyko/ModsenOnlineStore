using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
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
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IOrderProductRepository orderProductRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productTypeRepository = productTypeRepository;
            this.orderProductRepository = orderProductRepository;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<DataResponseInfo<List<GetProductDTO>>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            var products = await productRepository.GetAllProductsAsync(pageNumber, pageSize);
            var productDtos = products.Select(mapper.Map<GetProductDTO>).ToList();

            return new DataResponseInfo<List<GetProductDTO>>(data: productDtos, success: true, message: "all products");
        }

        public async Task<DataResponseInfo<GetProductDTO>> GetProductByIdAsync(int id)
        {
            var product = await productRepository.GetProductByIdAsync(id);

            if (product is null)
            {
                return new DataResponseInfo<GetProductDTO>(data: null, success: false, message: "no such product");
            }

            var productDto = mapper.Map<GetProductDTO>(product);

            return new DataResponseInfo<GetProductDTO>(data: productDto, success: true, message: "product");
        }

        public async Task<ResponseInfo> AddProductAsync(AddProductDTO addProductDto)
        {
            var productType = await productTypeRepository.GetSingleProductTypeAsync(addProductDto.ProductTypeId);

            if (productType is null)
            {
                return new ResponseInfo(success: false, message: "no such product type");
            }

            var product = mapper.Map<Product>(addProductDto);
            await productRepository.AddProductAsync(product);

            return new ResponseInfo(success: true, message: "product added");
        }

        public async Task<ResponseInfo> UpdateProductAsync(UpdateProductDTO updateProductDto)
        {
            var oldProduct = await productRepository.GetProductByIdAsync(updateProductDto.Id);

            if (oldProduct is null)
            {
                return new ResponseInfo(success: false, message: "no such product");
            }

            var productType = await productTypeRepository.GetSingleProductTypeAsync(updateProductDto.ProductTypeId);

            if (productType is null)
            {
                return new ResponseInfo(success: false, message: "no such product type");
            }

            var product = mapper.Map<Product>(updateProductDto);
            await productRepository.UpdateProductAsync(product);

            return new ResponseInfo(success: true, message: "product updated");
        }

        public async Task<ResponseInfo> RemoveProductByIdAsync(int id)
        {
            var product = await productRepository.GetProductByIdAsync(id);

            if (product is null)
            {
                return new ResponseInfo(success: false, message: "no such product");
            }

            await productRepository.RemoveProductByIdAsync(id);

            return new ResponseInfo(success: true, message: "product removed");
        }

        public async Task<DataResponseInfo<List<GetProductDTO>>> GetAllProductsByProductTypeIdAsync(int id, int pageNumber, int pageSize)
        {
            var productType = await productTypeRepository.GetSingleProductTypeAsync(id);

            if (productType is null)
            {
                return new DataResponseInfo<List<GetProductDTO>>(data: null, success: false, message: "no such product type");
            }

            var productTypeProducts = await productRepository.GetAllProductsByProductTypeIdAsync(id, pageNumber, pageSize);
            var productDtos = productTypeProducts.Select(mapper.Map<GetProductDTO>).ToList();

            return new DataResponseInfo<List<GetProductDTO>>(data: productDtos, success: true, message: "all products of product type");
        }

        public async Task<DataResponseInfo<List<GetProductDTO>>> GetAllProductsByOrderIdAsync(int id, int pageNumber, int pageSize)
        {
            var order = await orderRepository.GetSingleOrderAsync(id);

            if (order is null)
            {
                return new DataResponseInfo<List<GetProductDTO>>(data: null, success: false, message: "no such order");
            }

            var orderProducts = await orderProductRepository.GetAllOrderProductsByOrderIdAsync(id, pageNumber, pageSize);
            var productDtos = new List<GetProductDTO>();

            foreach (var orderProduct in orderProducts)
            {
                var product = await productRepository.GetProductByIdAsync(orderProduct.ProductId);
                var productDto = mapper.Map<GetProductDTO>(product);
                productDtos.Add(productDto);
            }

            return new DataResponseInfo<List<GetProductDTO>>(data: productDtos, success: true, message: "all products of order");
        }
    }
}
