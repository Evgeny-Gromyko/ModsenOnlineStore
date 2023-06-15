using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Domain.DTOs;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTO;

namespace ModsenOnlineStore.Store.Application.Services;

public class CouponService : ICouponService
{
    
    private IMapper mapper;
    private ICouponRepository repository;

    public CouponService(IMapper mapper, ICouponRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    
    public async Task<ResponseInfo<GetCouponDTO>> GetCoupon(int couponId)
    {
        throw new NotImplementedException();

    }

    public async Task<ResponseInfo<List<GetCouponDTO>>> GetAllCoupons()
    {
        var coupons = await repository.GetAllCoupons();
        var couponDtos = coupons.Select(mapper.Map<GetCouponDTO>).ToList(); 
        
        return new ResponseInfo<List<GetCouponDTO>>(couponDtos, true, "all coupons");
    }

    public async Task<ResponseInfo<List<GetCouponDTO>>> GetCouponsByUserId(int userId)
    {
        var coupons = await repository.GetCouponsByUserId(userId);
        if (coupons is null) {
            return new ResponseInfo<List<GetCouponDTO>>(null, false, "coupons not found");
        }
        var couponDtos = coupons.Select(mapper.Map<GetCouponDTO>).ToList();

        return new ResponseInfo<List<GetCouponDTO>>(couponDtos, true, $"product type with user id {userId}");
    }


    public Task<ResponseInfo<GetCouponDTO>> AddCoupon(AddCouponDTO newCoupon)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseInfo<GetCouponDTO>> DeleteCoupon(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseInfo<List<GetCouponDTO>>> DeleteCouponsByUserId(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseInfo<GetOrderDTO>> ApplyCoupon(int couponId, int orderId)
    {
        throw new NotImplementedException();
    }
}