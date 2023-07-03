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

        public async Task<DataResponseInfo<List<GetProductDTO>>> GetAllProducts()
        {
            var products = await productRepository.GetAllProducts();
            var productDtos = products.Select(mapper.Map<GetProductDTO>).ToList();

            return new DataResponseInfo<List<GetProductDTO>>(data: productDtos, success: true, message: "all products");
        }

        public async Task<DataResponseInfo<GetProductDTO>> GetProductById(int id)
        {
            var product = await productRepository.GetProductById(id);

            if (product is null)
            {
                return new DataResponseInfo<GetProductDTO>(data: null, success: false, message: "no such product");
            }

            var productDto = mapper.Map<GetProductDTO>(product);

            return new DataResponseInfo<GetProductDTO>(data: productDto, success: true, message: "product");
        }

        public async Task<ResponseInfo> AddProduct(AddProductDTO addProductDto)
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

        public async Task<ResponseInfo> UpdateProduct(UpdateProductDTO updateProductDto)
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

        public async Task<DataResponseInfo<List<GetProductDTO>>> GetAllProductsByProductTypeId(int id)
        {
            var productType = productTypeRepository.GetSingleProductType(id);

            if (productType is null)
            {
                return new DataResponseInfo<List<GetProductDTO>>(data: null, success: false, message: "no such product type");
            }

            var products = await productRepository.GetAllProducts();
            var productTypeProducts = products.FindAll(p => p.ProductTypeId == id);
            var productDtos = productTypeProducts.Select(mapper.Map<GetProductDTO>).ToList();

            return new DataResponseInfo<List<GetProductDTO>>(data: productDtos, success: true, message: "all products of product type");
        }

        public async Task<DataResponseInfo<List<GetProductDTO>>> GetAllProductsByOrderId(int id)
        {
            var order = orderRepository.GetSingleOrder(id);

            if (order is null)
            {
                return new DataResponseInfo<List<GetProductDTO>>(data: null, success: false, message: "no such order");
            }

            var allOrderProducts = await orderProductRepository.GetAllOrderProducts();
            var orderProducts = allOrderProducts.FindAll(o => o.OrderId == id);

            var productDtos = new List<GetProductDTO>();

            foreach (var orderProduct in orderProducts)
            {
                var product = await productRepository.GetProductById(orderProduct.ProductId);
                var productDto = mapper.Map<GetProductDTO>(product);
                productDtos.Add(productDto);
            }

            return new DataResponseInfo<List<GetProductDTO>>(data: productDtos, success: true, message: "all products of order");
        }
    }
}
