using ModsenOnlineStore.Store.Domain.DTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces;

public interface ICouponService
{
    Task<GetCouponDTO> GetCoupon(int couponId);
    Task<List<GetCouponDTO>> GetAllCoupons();
    Task<List<GetCouponDTO>> GetCouponsByUserId(int userId);
    Task<GetCouponDTO> AddCoupon(AddCouponDTO newCoupon);
    Task<GetCouponDTO> DeleteCoupon(int id);
    Task<List<GetCouponDTO>> DeleteCouponsByUserId(int id);
}