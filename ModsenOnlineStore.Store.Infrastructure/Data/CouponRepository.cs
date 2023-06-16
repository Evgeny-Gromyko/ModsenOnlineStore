using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data;

public class CouponRepository : ICouponRepository
{
    private DataContext context;
    
    public CouponRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<Coupon> GetCoupon(int couponId) =>
        await context.Coupons
            .AsNoTracking()
            .Include(c => c.User)
            .FirstOrDefaultAsync(e => e.Id == couponId);


    public async Task<List<Coupon>> GetAllCoupons() =>
        await context.Coupons
            .AsNoTracking()
            .Include(c => c.User)
            .ToListAsync();


    public async Task<List<Coupon>> GetCouponsByUserId(int userId) =>
        await context.Coupons.AsNoTracking()
            .Where(e => e.UserId == userId)
            .Include(c => c.User)
            .ToListAsync();


    public async Task<Coupon> AddCoupon(Coupon newCoupon)
    {
        context.Coupons.Add(newCoupon);
        await context.SaveChangesAsync();
        
        return newCoupon;
    }

    public async Task<Coupon> DeleteCoupon(int id)
    {
        var _coupon = await GetCoupon(id);
        if (_coupon is null) return null;

        context.Coupons.Remove(_coupon);
        await context.SaveChangesAsync();
        
        return _coupon;
    }

    public async Task<List<Coupon>> DeleteCouponsByUserId(int userId)
    {
        var _couponsToDelete = await context.Coupons
            .Where(c => c.UserId == userId)
            .ToListAsync();

        context.Coupons.RemoveRange(_couponsToDelete);
        await context.SaveChangesAsync();
        
        return _couponsToDelete;
    }

    public async Task<Order?> ApplyCoupon(int couponId, int orderId)
    {
        var coupon = await GetCoupon(couponId);
        
        var order = await context.Orders.
            Include(o => o.User).
            FirstOrDefaultAsync(p => p.Id == orderId);
        
        if (order is null)
            return null;

        order.TotalPrice -= coupon.Discount * order.TotalPrice;

        return order;
    }
}