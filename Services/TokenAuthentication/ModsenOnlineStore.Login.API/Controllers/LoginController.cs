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
        private readonly ILoginService service;
        private readonly IEncryptionService encryption;

        public LoginController(ILoginService service, IEncryptionService encryption)
        {
            this.service = service;
            this.encryption = encryption;
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(LoginData data)
        {
            data.Password = encryption.HashPassword(data.Password);
            var token = await service.GetToken(data);

            if (token is null) return Unauthorized();

            return Ok(new { access_token = token });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers() =>
            Ok((await service.GetAllUsers()).Data);

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSingleUser(int id)
        {
            var response = await service.GetUserById(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> RegisterUser(AddUserDto user)
        {
            user.Password = encryption.HashPassword(user.Password);
            return Ok(await service.RegisterUser(user));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserDto newEvent)
        {
            var response = await service.UpdateUser(newEvent);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await service.DeleteUser(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(int userId, string code)
        {
            var response = await service.ConfirmEmail(userId, code);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
