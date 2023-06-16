using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTO;
using ModsenOnlineStore.Store.Domain.Entities;

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
        var coupon = await repository.GetCoupon(couponId);
        
        if (coupon is null)
        {
            return new ResponseInfo<GetCouponDTO>(null, false, "not found");
        }

        var couponDTO = mapper.Map<GetCouponDTO>(coupon);
        
        return new ResponseInfo<GetCouponDTO>(couponDTO, true, $"coupon with id {couponId}");
    }

    public async Task<ResponseInfo<List<GetCouponDTO>>> GetAllCoupons()
    {
        var coupons = await repository.GetAllCoupons();
        
        if (coupons is null)
        {
            return new ResponseInfo<List<GetCouponDTO>>(null, false, "not found");
        }
        
        var couponDtos = coupons.Select(mapper.Map<GetCouponDTO>).ToList(); 
        
        return new ResponseInfo<List<GetCouponDTO>>(couponDtos, true, "all coupons");
    }

    public async Task<ResponseInfo<List<GetCouponDTO>>> GetCouponsByUserId(int userId)
    {
        var coupons = await repository.GetCouponsByUserId(userId);
        
        if (coupons is null)
        {
            return new ResponseInfo<List<GetCouponDTO>>(null, false, "coupons not found");
        }
        
        var couponDtos = coupons.Select(mapper.Map<GetCouponDTO>).ToList();

        return new ResponseInfo<List<GetCouponDTO>>(couponDtos, true, $"product type with user id {userId}");
    }


    public async Task<OperationResult> AddCoupon(AddCouponDTO newCouponDto)
    {

        var newCoupon= mapper.Map<Coupon>(newCouponDto);

        var events = await repository.AddCoupon(newCoupon);

        return new OperationResult( true, "coupon added");

    }

    public async Task<OperationResult> DeleteCoupon(int id)
    {
        var coupon = await repository.DeleteCoupon(id);
            
        if (coupon is null)
        {
            return new OperationResult(false, "coupon not found");
        }
        
        return new OperationResult(true, "type deleted successfully");
    }

    public async Task<OperationResult> DeleteCouponsByUserId(int id)
    {
        var couponList = await repository.DeleteCouponsByUserId(id);
            
        if (couponList is null)
        {
            return new OperationResult(false, "coupons not found");
        }
    
        return new OperationResult(true, "coupons deleted successfully");
    }

    public async Task<ResponseInfo<GetOrderDTO>> ApplyCoupon(ApplyCouponDTO dto)
    {
        var order = await repository.ApplyCoupon(dto.couponId, dto.orderId);

        if (order is null)
        {
            return new ResponseInfo<GetOrderDTO>(null, false, "not found");
        }

        var orderDto = mapper.Map<GetOrderDTO>(order);

        return new ResponseInfo<GetOrderDTO>(orderDto, true, $"coupon with id {dto.couponId} applied");
    }
}