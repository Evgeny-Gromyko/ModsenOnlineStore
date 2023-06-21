using AutoMapper;
using ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.API;

public class OrderProductProfile : Profile
{
    public OrderProductProfile()
    {
        CreateMap<OrderProduct, GetOrderProductDTO>();
    }
}