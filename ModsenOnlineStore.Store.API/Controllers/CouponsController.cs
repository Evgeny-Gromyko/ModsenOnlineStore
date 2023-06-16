using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;

namespace ModsenOnlineStore.Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private ICouponService couponService;

        public CouponsController(ICouponService couponService)
        {
            this.couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupons() =>
            Ok(await couponService.GetAllCoupons());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoupon(int id)
        {
            var couponInfo = await couponService.GetCoupon(id);

            if (couponInfo.Success is false)
            {
                return NotFound();
            }

            return Ok(couponInfo);
        }

        [HttpPost]
        public async Task<IActionResult> AddCoupon(AddCouponDTO coupon) =>
            Ok(await couponService.AddCoupon(coupon));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var couponInfo = await couponService.DeleteCoupon(id);

            if (couponInfo.IsSuccess is false)
            {
                return NotFound();
            }

            return Ok(couponInfo);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(ApplyCouponDTO data)
        {
            return Ok(await couponService.ApplyCoupon(data));
        }
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCouponsByUserId(int userId)
        {
            var couponInfo = await couponService.GetCouponsByUserId(userId);
            
            if (couponInfo.Success is false)
            {
                return NotFound();
            }
            
            return Ok(couponInfo);
        }
        
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteCouponsByUserId(int userId)
        {
            var couponInfo = await couponService.DeleteCouponsByUserId(userId);
            
            if (couponInfo.IsSuccess is false)
            {
                return NotFound();
            }
            
            return Ok(couponInfo);
        }

    }
}
