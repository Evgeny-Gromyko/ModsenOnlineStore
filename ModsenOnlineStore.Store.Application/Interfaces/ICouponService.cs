using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTO;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;

namespace ModsenOnlineStore.Store.Application.Interfaces;

public interface ICouponService
{
    Task<ResponseInfo> GetCoupon(int couponId);
    
    Task<ResponseInfo> GetAllCoupons();
    
    Task<ResponseInfo> GetCouponsByUserId(int userId);
    
    Task<ResponseInfo> AddCoupon(AddCouponDTO newCoupon);
    
    Task<ResponseInfo> DeleteCoupon(int id);
    
    Task<ResponseInfo> DeleteCouponsByUserId(int id);
    
    Task<ResponseInfo> ApplyCoupon(ApplyCouponDTO dto);
}