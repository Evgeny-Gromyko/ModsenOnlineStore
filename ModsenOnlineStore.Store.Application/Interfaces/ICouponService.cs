using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTO;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;

namespace ModsenOnlineStore.Store.Application.Interfaces;

public interface ICouponService
{
    Task<ResponseInfo<GetCouponDTO>> GetCoupon(int couponId);
    
    Task<ResponseInfo<List<GetCouponDTO>>> GetAllCoupons();
    
    Task<ResponseInfo<List<GetCouponDTO>>> GetCouponsByUserId(int userId);
    
    Task<OperationResult> AddCoupon(AddCouponDTO newCoupon);
    
    Task<OperationResult> DeleteCoupon(int id);
    
    Task<OperationResult> DeleteCouponsByUserId(int id);
    
    Task<ResponseInfo<GetOrderDTO>> ApplyCoupon(ApplyCouponDTO dto);
}