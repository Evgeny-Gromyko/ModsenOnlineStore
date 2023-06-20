using AutoMapper;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTO;
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