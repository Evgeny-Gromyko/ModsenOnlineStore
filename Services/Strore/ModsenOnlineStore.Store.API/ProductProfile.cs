using AutoMapper;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.API
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, GetProductDto>();
        }
    }
}
