using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using ModsenOnlineStore.Store.Application.Interfaces.CouponInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Services.CouponServices;

public class CouponService : ICouponService
{
    private readonly IMapper mapper;
    private readonly ICouponRepository couponRepository;
    private readonly IOrderRepository orderRepository;

    public CouponService(IMapper mapper, ICouponRepository couponRepository, IOrderRepository orderRepository)
    {
        this.mapper = mapper;
        this.couponRepository = couponRepository;
        this.orderRepository = orderRepository;
    }
    
    public async Task<DataResponseInfo<GetCouponDTO>> GetCouponAsync(int couponId)
    {
        var coupon = await couponRepository.GetCouponAsync(couponId);
        
        if (coupon is null)
        {
            return new DataResponseInfo<GetCouponDTO>(data: null, success: false, message: "not found");
        }

        var couponDTO = mapper.Map<GetCouponDTO>(coupon);
        
        return new DataResponseInfo<GetCouponDTO>(data: couponDTO, success: true, message: $"coupon with id {couponId}");
    }

    public async Task<DataResponseInfo<List<GetCouponDTO>>> GetAllCouponsAsync()
    {
        var coupons = await couponRepository.GetAllCouponsAsync();
        var couponDtos = coupons.Select(mapper.Map<GetCouponDTO>).ToList(); 
        
        return new DataResponseInfo<List<GetCouponDTO>>(data: couponDtos, success: true, message: "all coupons");
    }

    public async Task<DataResponseInfo<List<GetCouponDTO>>> GetCouponsByUserIdAsync(int userId)
    {
        var coupons = await couponRepository.GetCouponsByUserIdAsync(userId);
        
        if (coupons is null)
        {
            return new DataResponseInfo<List<GetCouponDTO>>(data: null, success: false, message: "coupons not found");
        }
        
        var couponDtos = coupons.Select(mapper.Map<GetCouponDTO>).ToList();

        return new DataResponseInfo<List<GetCouponDTO>>(data: couponDtos, success: true, message: $"coupons of user with id {userId}");
    }

    public async Task<ResponseInfo> AddCouponAsync(AddCouponDTO newCouponDto)
    {
        var newCoupon= mapper.Map<Coupon>(newCouponDto);
        await couponRepository.AddCouponAsync(newCoupon);

        return new ResponseInfo(success: true, message: "coupon added");
    }

    public async Task<ResponseInfo> DeleteCouponAsync(int id)
    {
        var coupon = await couponRepository.DeleteCouponAsync(id);
            
        if (coupon is null)
        {
            return new ResponseInfo(success: false, message: "coupon not found");
        }
        
        return new ResponseInfo(success: true, message: "type deleted successfully");
    }

    public async Task<ResponseInfo> DeleteCouponsByUserIdAsync(int id)
    {
        var couponList = await couponRepository.DeleteCouponsByUserIdAsync(id);

        if (couponList.Count == 0)
        {
            return new ResponseInfo(success: false, message: "coupons not found");
        }
    
        return new ResponseInfo(success: true, message: "coupons deleted successfully");
    }

    public async Task<ResponseInfo> ApplyCouponAsync(ApplyCouponDTO dto)
    {
        var coupon = await couponRepository.GetCouponAsync(dto.CouponId);

        var order = await orderRepository.GetSingleOrderAsync(dto.OrderId);

        if (order is null || coupon is null)
        {
           return new ResponseInfo(success: false, message: "not found");
        }

        if (coupon.UserId != order.UserId)
        {
            return new ResponseInfo(success: false, message: "coupon and order are from different users");
        }

        order.TotalPrice -= coupon.Discount * order.TotalPrice / 100;

        await orderRepository.UpdateOrderAsync(order);
        await couponRepository.DeleteCouponAsync(dto.CouponId);
        
        return new ResponseInfo(success: true, message: $"coupon with id {dto.CouponId} is applied");
    }
}
