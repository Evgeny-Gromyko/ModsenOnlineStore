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
    
    public async Task<ResponseInfo<GetCouponDTO>> GetCoupon(int couponId)
    {
        var coupon = await couponRepository.GetCoupon(couponId);
        
        if (coupon is null)
        {
            return new ResponseInfo<GetCouponDTO>(null, false, "not found");
        }

        var couponDTO = mapper.Map<GetCouponDTO>(coupon);
        
        return new ResponseInfo<GetCouponDTO>(couponDTO, true, $"coupon with id {couponId}");
    }

    public async Task<ResponseInfo<List<GetCouponDTO>>> GetAllCoupons()
    {
        var coupons = await couponRepository.GetAllCoupons();
        
        if (coupons is null)
        {
            return new ResponseInfo<List<GetCouponDTO>>(null, false, "not found");
        }
        
        var couponDtos = coupons.Select(mapper.Map<GetCouponDTO>).ToList(); 
        
        return new ResponseInfo<List<GetCouponDTO>>(couponDtos, true, "all coupons");
    }

    public async Task<ResponseInfo<List<GetCouponDTO>>> GetCouponsByUserId(int userId)
    {
        var coupons = await couponRepository.GetCouponsByUserId(userId);
        
        if (coupons is null)
        {
            return new ResponseInfo<List<GetCouponDTO>>(null, false, "coupons not found");
        }
        
        var couponDtos = coupons.Select(mapper.Map<GetCouponDTO>).ToList();

        return new ResponseInfo<List<GetCouponDTO>>(couponDtos, true, $"product type with user id {userId}");
    }


    public async Task<NoDataResponseInfo> AddCoupon(AddCouponDTO newCouponDto)
    {

        var newCoupon= mapper.Map<Coupon>(newCouponDto);

        var events = await couponRepository.AddCoupon(newCoupon);

        return new NoDataResponseInfo( true, "coupon added");

    }

    public async Task<NoDataResponseInfo> DeleteCoupon(int id)
    {
        var coupon = await couponRepository.DeleteCoupon(id);
            
        if (coupon is null)
        {
            return new NoDataResponseInfo(false, "coupon not found");
        }
        
        return new NoDataResponseInfo(true, "type deleted successfully");
    }

    public async Task<NoDataResponseInfo> DeleteCouponsByUserId(int id)
    {
        var couponList = await couponRepository.DeleteCouponsByUserId(id);
            
        if (couponList is null)
        {
            return new NoDataResponseInfo(false, "coupons not found");
        }
    
        return new NoDataResponseInfo(true, "coupons deleted successfully");
    }

    public async Task<NoDataResponseInfo> ApplyCoupon(ApplyCouponDTO dto)
    {
        var coupon = await couponRepository.GetCoupon(dto.CouponId);

        var order = await orderRepository.GetSingleOrder(dto.OrderId);

        if (order is null || coupon is null)
        {
           return new NoDataResponseInfo(false, "not found");
        }

        if (coupon.UserId != order.UserId)
        {
            return new NoDataResponseInfo(false, "coupon and order are from different users");
        }

        order.TotalPrice -= coupon.Discount * order.TotalPrice / 100;

        await orderRepository.UpdateOrder(order);
        await couponRepository.DeleteCoupon(dto.CouponId);
        
        return new NoDataResponseInfo(true, $"coupon with id {dto.CouponId} is applied");
    }
}