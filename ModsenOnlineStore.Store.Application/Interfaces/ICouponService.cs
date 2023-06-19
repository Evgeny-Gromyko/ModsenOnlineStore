using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTO;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;

namespace ModsenOnlineStore.Store.Application.Interfaces;

public interface ICouponService
{
    Task<ResponseInfo<GetCouponDTO>> GetCoupon(int couponId);
    
    Task<ResponseInfo<List<GetCouponDTO>>> GetAllCoupons();
    
    Task<ResponseInfo<List<GetCouponDTO>>> GetCouponsByUserId(int userId);
    
    Task<NoDataResponseInfo> AddCoupon(AddCouponDTO newCoupon);
    
    Task<NoDataResponseInfo> DeleteCoupon(int id);
    
    Task<NoDataResponseInfo> DeleteCouponsByUserId(int id);
    
    Task<NoDataResponseInfo> ApplyCoupon(ApplyCouponDTO dto);
}