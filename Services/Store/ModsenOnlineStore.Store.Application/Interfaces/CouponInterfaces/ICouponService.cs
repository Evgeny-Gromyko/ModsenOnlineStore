using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.CouponInterfaces;

public interface ICouponService
{
    Task<DataResponseInfo<GetCouponDTO>> GetCouponAsync(int couponId);
    
    Task<DataResponseInfo<List<GetCouponDTO>>> GetAllCouponsAsync(int pageNumber, int pageSize);
    
    Task<DataResponseInfo<List<GetCouponDTO>>> GetCouponsByUserIdAsync(int userId, int pageNumber, int pageSize);
    
    Task<ResponseInfo> AddCouponAsync(AddCouponDTO newCoupon);
    
    Task<ResponseInfo> DeleteCouponAsync(int id);
    
    Task<ResponseInfo> DeleteCouponsByUserIdAsync(int id);
    
    Task<ResponseInfo> ApplyCouponAsync(ApplyCouponDTO dto);
}
