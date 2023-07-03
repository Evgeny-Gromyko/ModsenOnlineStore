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
        public async Task<IActionResult> GetAllCouponsAsync()
        {
            var response = await couponService.GetAllCouponsAsync();
            
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCouponAsync(int id)
        {
            var response = await couponService.GetCouponAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCouponAsync(AddCouponDTO coupon)
        {
            var response = await couponService.AddCouponAsync(coupon);
            
            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCouponAsync(int id)
        {
            var response = await couponService.DeleteCouponAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPost("apply")]
        [Authorize]
        public async Task<IActionResult> ApplyCouponAsync(ApplyCouponDTO data)
        {
            var response = await couponService.ApplyCouponAsync(data);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
        
        [HttpGet("byUser{userId}")]
        [Authorize]
        public async Task<IActionResult> GetCouponsByUserIdAsync(int userId)
        {
            var response = await couponService.GetCouponsByUserIdAsync(userId);
            
            return Ok(response.Data);
        }
        
        [HttpDelete("byUser{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCouponsByUserIdAsync(int userId)
        {
            var response = await couponService.DeleteCouponsByUserIdAsync(userId);
            
            return Ok(response.Message);
        }

    }
}
