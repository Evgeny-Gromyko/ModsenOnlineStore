using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Login.Application.Interfaces;
using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;
using ModsenOnlineStore.Login.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Login.Infrastructure.Services
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
            if (user is null) return null;

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

            return new DataResponseInfo<string>(new JwtSecurityTokenHandler().WriteToken(jwt), true, "token");
        }

        public async Task<DataResponseInfo<List<User>>> GetAllUsers() =>
            new DataResponseInfo<List<User>>(await repository.GetAllUsers(), true, "all users");

        public async Task<DataResponseInfo<User>> GetUserById(int id)
        {
            var user = await repository.GetUserById(id);
            if (user is null) return new DataResponseInfo<User>(null, true, "user not found");

            return new DataResponseInfo<User>(user, true, $"user with id {user.Id}");
        }


        public async Task<DataResponseInfo<List<User>>> RegisterUser(AddUserDto userDto)
        {
            if (userDto is null) return new DataResponseInfo<List<User>>(null, false, "wrong request data");
            var newUser = mapper.Map<User>(userDto);

            var users = await repository.RegisterUser(newUser);

            return new DataResponseInfo<List<User>>(users, true, "all users");
        }

        public async Task<DataResponseInfo<List<User>>> DeleteUser(int id)
        {
            var users = await repository.DeleteUser(id);
            if (users is null) return new DataResponseInfo<List<User>>(null, false, "user not found");

            return new DataResponseInfo<List<User>>(users, true, "all users");
        }

        public async Task<DataResponseInfo<User>> UpdateUser(UpdateUserDto userDto)
        {
            if (userDto == null) return new DataResponseInfo<User>(null, false, "wrong request data");
            var newUser = mapper.Map<User>(userDto);

            var response = await repository.EditUser(newUser);
            if (response is null) return new DataResponseInfo<User>(null, false, "event not found");

            return new DataResponseInfo<User>(response, true, $"user with id {response.Id}");
        }
    }
}
