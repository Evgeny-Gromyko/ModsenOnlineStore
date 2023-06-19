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
    private ICouponRepository couponRepository;
    private IOrderRepository orderRepository;


    public CouponService(IMapper mapper, ICouponRepository couponRepository, IOrderRepository orderRepository)
    {
        this.mapper = mapper;
        this.couponRepository = couponRepository;
        this.orderRepository = orderRepository;
    }
    
    public async Task<ResponseInfo> GetCoupon(int couponId)
    {
        var coupon = await couponRepository.GetCoupon(couponId);
        
        if (coupon is null)
        {
            return new ResponseInfo(false, "not found");
        }

        var couponDTO = mapper.Map<GetCouponDTO>(coupon);
        
        return new DataResponseInfo<GetCouponDTO>(couponDTO, true, $"coupon with id {couponId}");
    }

    public async Task<ResponseInfo> GetAllCoupons()
    {
        var coupons = await couponRepository.GetAllCoupons();
        
        if (coupons is null)
        {
            return new ResponseInfo( false, "not found");
        }
        
        var couponDtos = coupons.Select(mapper.Map<GetCouponDTO>).ToList(); 
        
        return new DataResponseInfo<List<GetCouponDTO>>(couponDtos, true, "all coupons");
    }

    public async Task<ResponseInfo> GetCouponsByUserId(int userId)
    {
        var coupons = await couponRepository.GetCouponsByUserId(userId);
        
        if (coupons is null)
        {
            return new ResponseInfo( false, "coupons not found");
        }
        
        var couponDtos = coupons.Select(mapper.Map<GetCouponDTO>).ToList();

        return new DataResponseInfo<List<GetCouponDTO>>(couponDtos, true, $"product type with user id {userId}");
    }


    public async Task<ResponseInfo> AddCoupon(AddCouponDTO newCouponDto)
    {

        var newCoupon= mapper.Map<Coupon>(newCouponDto);

        var events = await couponRepository.AddCoupon(newCoupon);

        return new ResponseInfo( true, "coupon added");

    }

    public async Task<ResponseInfo> DeleteCoupon(int id)
    {
        var coupon = await couponRepository.DeleteCoupon(id);
            
        if (coupon is null)
        {
            return new ResponseInfo(false, "coupon not found");
        }
        
        return new ResponseInfo(true, "type deleted successfully");
    }

    public async Task<ResponseInfo> DeleteCouponsByUserId(int id)
    {
        var couponList = await couponRepository.DeleteCouponsByUserId(id);
            
        if (couponList is null)
        {
            return new ResponseInfo(false, "coupons not found");
        }
    
        return new ResponseInfo(true, "coupons deleted successfully");
    }

    public async Task<ResponseInfo> ApplyCoupon(ApplyCouponDTO dto)
    {
        var coupon = await couponRepository.GetCoupon(dto.CouponId);

        var order = await orderRepository.GetSingleOrder(dto.OrderId);

        if (order is null || coupon is null)
        {
           return new ResponseInfo(false, "not found");
        }

        if (coupon.UserId != order.UserId)
        {
            return new ResponseInfo(false, "coupon and order are from different users");
        }

        order.TotalPrice -= coupon.Discount * order.TotalPrice / 100;

        await orderRepository.UpdateOrder(order);
        await couponRepository.DeleteCoupon(dto.CouponId);
        
        return new ResponseInfo(true, $"coupon with id {dto.CouponId} is applied");
    }
}