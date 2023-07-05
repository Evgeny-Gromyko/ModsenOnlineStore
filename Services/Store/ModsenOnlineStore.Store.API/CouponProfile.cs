using AutoMapper;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTOs;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.API;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<AddCouponDTO, Coupon>();
        CreateMap<Coupon, GetCouponDTO>();
        CreateMap<Order, GetOrderDTO>();
    }
}
