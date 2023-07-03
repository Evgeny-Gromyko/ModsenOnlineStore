using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.CouponInterfaces;

public interface ICouponService
{
    Task<DataResponseInfo<GetCouponDTO>> GetCouponAsync(int couponId);
    
    Task<DataResponseInfo<List<GetCouponDTO>>> GetAllCouponsAsync();
    
    Task<DataResponseInfo<List<GetCouponDTO>>> GetCouponsByUserIdAsync(int userId);
    
    Task<ResponseInfo> AddCouponAsync(AddCouponDTO newCoupon);
    
    Task<ResponseInfo> DeleteCouponAsync(int id);
    
    Task<ResponseInfo> DeleteCouponsByUserIdAsync(int id);
    
    Task<ResponseInfo> ApplyCouponAsync(ApplyCouponDTO dto);
}
