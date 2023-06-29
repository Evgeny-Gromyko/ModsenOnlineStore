using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Login.Application.Interfaces;
using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;
using ModsenOnlineStore.Login.Domain.Entities;

namespace ModsenOnlineStore.Login.Application.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository repository;
        private IOptions<AuthOptions> authOptions;
        private IMapper mapper;

        public LoginService(IUserRepository repository, IOptions<AuthOptions> authOptions, IMapper mapper)
        {
            this.repository = repository;
            this.authOptions = authOptions;
            this.mapper = mapper;
        }

        public async Task<DataResponseInfo<string>> GetToken(LoginData data)
        {
            User user = await repository.AuthenticateUser(data.Email, data.Password);
            
            if (user is null) return new DataResponseInfo<string>(data: null, success: false, message: "user is not found");

            var authParams = authOptions.Value;

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.Role.ToString())
            };

            var jwt = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(authParams.TokenLifeTime)),
                signingCredentials: new SigningCredentials(
                    authParams.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return new DataResponseInfo<string>(data: new JwtSecurityTokenHandler().WriteToken(jwt), success: true, message: "token");
        }

        public async Task<DataResponseInfo<List<User>>> GetAllUsers() =>
            new DataResponseInfo<List<User>>(await repository.GetAllUsers(), true, "all users");

        public async Task<DataResponseInfo<User>> GetUserById(int id)
        {
            var user = await repository.GetUserById(id);
            
            if (user is null) return new DataResponseInfo<User>(null, false, $"user with id {id} not found");

            return new DataResponseInfo<User>(user, true, $"user with id {user.Id}");
        }


        public async Task<ResponseInfo> RegisterUser(AddUserDto userDto)
        {
            if (userDto is null) return new ResponseInfo(false, "wrong request data");
            
            var newUser = mapper.Map<User>(userDto);
            var user = await repository.RegisterUser(newUser);

            return new ResponseInfo(true, $"user with id {newUser.Id} registered");
        }

        public async Task<ResponseInfo> DeleteUser(int id)
        {
            var user = await repository.DeleteUser(id);
            
            if (user is null) return new ResponseInfo(false, $"user with id {id} not found");

            return new ResponseInfo(true, $"user with id {id} deleted");
        }

        public async Task<ResponseInfo> UpdateUser(UpdateUserDto userDto)
        {
            if (userDto == null) return new ResponseInfo(false, "wrong request data");
            
            var newUser = mapper.Map<User>(userDto);
            var response = await repository.EditUser(newUser);
            
            if (response is null) return new ResponseInfo(false, $"user with id {newUser.Id} not found");

            return new ResponseInfo(true, $"user with id {response.Id} updated");
        }
    }
}
