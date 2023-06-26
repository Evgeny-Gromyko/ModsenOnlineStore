using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces.CouponInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;

namespace ModsenOnlineStore.Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService couponService;

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
            var response = await couponService.GetCoupon(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddCoupon(AddCouponDTO coupon) =>
            Ok(await couponService.AddCoupon(coupon));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var response = await couponService.DeleteCoupon(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyCoupon(ApplyCouponDTO data)
        {
            var response = await couponService.ApplyCoupon(data);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpGet("byUser{userId}")]
        public async Task<IActionResult> GetCouponsByUserId(int userId)
        {   
            return Ok(await couponService.GetCouponsByUserId(userId));
        }
        
        [HttpDelete("byUser{userId}")]
        public async Task<IActionResult> DeleteCouponsByUserId(int userId)
        {   
            return Ok(await couponService.DeleteCouponsByUserId(userId));
        }

    }
}
