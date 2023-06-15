using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTO;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;

namespace ModsenOnlineStore.Store.Application.Interfaces;

public interface ICouponService
{
    Task<ResponseInfo<GetCouponDTO>> GetCoupon(int couponId);
    Task<ResponseInfo<List<GetCouponDTO>>> GetAllCoupons();
    Task<ResponseInfo<List<GetCouponDTO>>> GetCouponsByUserId(int userId);
    Task<ResponseInfo<GetCouponDTO>> AddCoupon(AddCouponDTO newCoupon);
    Task<ResponseInfo<GetCouponDTO>> DeleteCoupon(int id);
    Task<ResponseInfo<List<GetCouponDTO>>> DeleteCouponsByUserId(int id);

    Task<ResponseInfo<GetOrderDTO>> ApplyCoupon(int couponId, int orderId);
}