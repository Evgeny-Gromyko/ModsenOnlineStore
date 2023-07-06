using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Login.Application.Interfaces;
using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;

namespace ModsenOnlineStore.Login.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly IEncryptionService encryption;
        private readonly IUserMoneyService userMoneyService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LoginController(ILoginService loginService, IUserMoneyService userMoneyService, IEncryptionService encryption, IHttpContextAccessor httpContextAccessor)
        {
            this.loginService = loginService;
            this.encryption = encryption;
            this.userMoneyService = userMoneyService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> LoginAsync(LoginData data)
        {
            data.Password = encryption.HashPassword(data.Password);
            var response = await loginService.GetTokenAsync(data);

            if (response.Data is null) return Unauthorized(response.Message);

            return Ok(new { access_token = response.Data });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {


            var response = await loginService.GetAllUsersAsync(pageNumber, pageSize);

            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSingleUserAsync(int id)
        {
            var response = await loginService.GetUserByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> RegisterUserAsync(AddUserDTO user)
        {
            user.Password = encryption.HashPassword(user.Password);

            var response = await loginService.RegisterUserAsync(user, httpContextAccessor.HttpContext!.Request);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPost("/NewPayment/{userId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MakePaymentAsync(int userId, decimal money)
        {
            var response = await userMoneyService.MakePaymentAsync(userId, money);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("/AddMoney/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMoneyAsync(int userId, decimal money)
        {
            var response = await userMoneyService.AddMoneyAsync(userId, money);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserDTO user)
        {
            user.Password = encryption.HashPassword(user.Password);

            var response = await loginService.UpdateUserAsync(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var response = await loginService.DeleteUserAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync(int userId, string code)
        {
            var response = await loginService.ConfirmEmailAsync(userId, code);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
