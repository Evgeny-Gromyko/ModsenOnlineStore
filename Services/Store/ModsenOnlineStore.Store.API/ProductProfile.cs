using AutoMapper;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.API
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();
            CreateMap<Product, GetProductDTO>();
        }
    }
}
