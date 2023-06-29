using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Login.Application.Interfaces;
using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;

namespace ModsenOnlineStore.Login.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService service;
        private IEncryptionService encryption;

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
            var response = await service.GetToken(data);

            if (response.Data is null) return Unauthorized();

            return Ok(new { access_token = response.Data });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await service.GetAllUsers();
            
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSingleUser(int id)
        {
            var response = await service.GetUserById(id);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> RegisterUser(AddUserDto user)
        {
            user.Password = encryption.HashPassword(user.Password);

            var response = await service.RegisterUser(user);

            if (!response.Success)
            {
                return BadRequest();
            }
            
            return Ok(response.Message);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto user)
        {
            user.Password = encryption.HashPassword(user.Password);

            var response = await service.UpdateUser(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await service.DeleteUser(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }
    }
}
