using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCoupons()
        {
            var response = await couponService.GetAllCoupons();
            
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCoupon(int id)
        {
            var response = await couponService.GetCoupon(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCoupon(AddCouponDTO coupon)
        {
            var response = await couponService.AddCoupon(coupon);
            
            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var response = await couponService.DeleteCoupon(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPost("apply")]
        [Authorize]
        public async Task<IActionResult> ApplyCoupon(ApplyCouponDTO data)
        {
            var response = await couponService.ApplyCoupon(data);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpGet("byUser{userId}")]
        [Authorize]
        public async Task<IActionResult> GetCouponsByUserId(int userId)
        {
            var response = await couponService.GetCouponsByUserId(userId);
            
            return Ok(response.Data);
        }
        
        [HttpDelete("byUser{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCouponsByUserId(int userId)
        {
            var response = await couponService.DeleteCouponsByUserId(userId);
            
            return Ok(response.Message);
        }

    }
}
