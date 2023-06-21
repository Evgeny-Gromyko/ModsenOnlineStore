using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.CouponInterfaces;

public interface ICouponRepository
{
    Task<Coupon> GetCoupon(int couponId);
    Task<List<Coupon>> GetAllCoupons();
    Task<List<Coupon>> GetCouponsByUserId(int userId);
    Task<Coupon> AddCoupon(Coupon newCoupon);
    Task<Coupon> DeleteCoupon(int id);
    Task<List<Coupon>> DeleteCouponsByUserId(int id);
}