using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;

namespace ModsenOnlineStore.Store.Application.Interfaces.CouponInterfaces;

public interface ICouponService
{
    Task<DataResponseInfo<GetCouponDTO>> GetCoupon(int couponId);
    
    Task<DataResponseInfo<List<GetCouponDTO>>> GetAllCoupons();
    
    Task<DataResponseInfo<List<GetCouponDTO>>> GetCouponsByUserId(int userId);
    
    Task<ResponseInfo> AddCoupon(AddCouponDTO newCoupon);
    
    Task<ResponseInfo> DeleteCoupon(int id);
    
    Task<ResponseInfo> DeleteCouponsByUserId(int id);
    
    Task<ResponseInfo> ApplyCoupon(ApplyCouponDTO dto);
}