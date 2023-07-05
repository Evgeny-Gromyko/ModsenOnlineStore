using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.CouponInterfaces;

public interface ICouponRepository
{
    Task<Coupon> GetCouponAsync(int couponId);

    Task<List<Coupon>> GetAllCouponsAsync();

    Task<List<Coupon>> GetCouponsByUserIdAsync(int userId);

    Task<Coupon> AddCouponAsync(Coupon newCoupon);

    Task<Coupon> DeleteCouponAsync(int id);

    Task<List<Coupon>> DeleteCouponsByUserIdAsync(int id);
}
