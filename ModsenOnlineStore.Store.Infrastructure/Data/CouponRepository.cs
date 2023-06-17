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
        await context.Coupons
            .AsNoTracking()
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
        var coupon = await GetCoupon(id);
        
        if (coupon is null) return null;

        context.Coupons.Remove(coupon);
        await context.SaveChangesAsync();
        
        return coupon;
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
        
        var order = await context.Orders
            .FirstOrDefaultAsync(p => p.Id == orderId);
        
        if (order is null || coupon is null)
            return null;

        if (coupon.UserId != order.UserId)
            return null;

        order.TotalPrice -= coupon.Discount * order.TotalPrice/100;

        await context.SaveChangesAsync();
        await DeleteCoupon(couponId);
        
        return order;
    }
}