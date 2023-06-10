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
            var token = await service.GetToken(data);

            if (token is null) return Unauthorized();

            return Ok(new { access_token = token });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers() =>
            Ok(await service.GetAllUsers());

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSingleUser(int id) =>
            Ok(await service.GetUserById(id));

        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> RegisterUser(AddUserDto user)
        {
            user.Password = encryption.HashPassword(user.Password);
            return Ok(await service.RegisterUser(user));
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserDto newEvent) =>
            Ok(await service.UpdateUser(newEvent));

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(int id) =>
            Ok(await service.DeleteUser(id));
    }
}
