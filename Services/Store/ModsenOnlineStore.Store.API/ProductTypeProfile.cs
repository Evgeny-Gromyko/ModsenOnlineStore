using AutoMapper;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.API;

public class ProductTypeProfile : Profile
{
    public ProductTypeProfile()
    {
        CreateMap<AddUpdateProductTypeDTO, ProductType>();
        CreateMap<ProductType, GetProductTypeDTO>();
    }
}