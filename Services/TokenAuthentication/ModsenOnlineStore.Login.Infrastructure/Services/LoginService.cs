using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Login.Application.Interfaces;
using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;
using ModsenOnlineStore.Login.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ModsenOnlineStore.Login.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository repository;
        private readonly IEmailConfirmationRepository emailConfirmationRepository;
        private readonly IRabbitMQMessagingService rabbitMQMessagingService;
        private readonly IOptions<AuthOptions> authOptions;
        private readonly IMapper mapper;

        public LoginService(IUserRepository repository,
                            IEmailConfirmationRepository emailConfirmationRepository,
                            IRabbitMQMessagingService rabbitMQMessagingService,
                            IOptions<AuthOptions> authOptions,
                            IMapper mapper)
        {
            this.repository = repository;
            this.emailConfirmationRepository = emailConfirmationRepository;
            this.rabbitMQMessagingService = rabbitMQMessagingService;
            this.authOptions = authOptions;
            this.mapper = mapper;
        }

        public async Task<DataResponseInfo<string>> GetToken(LoginData data)
        {
            var user = await repository.AuthenticateUser(data.Email, data.Password);
            if (user is null) return null;

            if (!user.IsEmailConfirmed)
            {
                return new DataResponseInfo<string>(data: null, success: false, message: "user email not confirmed");
            }

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
            new DataResponseInfo<List<User>>(data: await repository.GetAllUsers(), success: true, message: "all users");

        public async Task<DataResponseInfo<User>> GetUserById(int id)
        {
            var user = await repository.GetUserById(id);
            if (user is null) return new DataResponseInfo<User>(data: null, success: false, message: "user not found");

            return new DataResponseInfo<User>(data: user, success: true, message: $"user with id {user.Id}");
        }

        public async Task<ResponseInfo> RegisterUser(AddUserDto userDto)
        {
            if (userDto is null) return new ResponseInfo(success: false, message: "wrong request data");

            var user = await repository.GetUserByEmail(userDto.Email);

            if (user is not null)
            {
                return new ResponseInfo(success: false, message: "this email is already in use");
            }

            var newUser = mapper.Map<User>(userDto);

            await repository.RegisterUser(newUser);

            user = await repository.GetUserByEmail(newUser.Email);

            var emailConfirmation = new EmailConfirmation
            {
                UserId = user!.Id,
                Code = Guid.NewGuid().ToString()
            };

            await emailConfirmationRepository.AddEmailConfirmation(emailConfirmation);

            var message = $"{user.Email} {user.Id} {emailConfirmation.Code}";
            rabbitMQMessagingService.PublishMessage("email-confirmation", message);

            return new ResponseInfo(success: true, message: "user registered");
        }

        public async Task<ResponseInfo> DeleteUser(int id)
        {
            var user = await repository.GetUserById(id);

            if (user is null) return new ResponseInfo(success: false, message: "user not found");

            await repository.DeleteUser(id);

            return new ResponseInfo(success: true, message: "user removed");
        }

        public async Task<ResponseInfo> UpdateUser(UpdateUserDto userDto)
        {
            if (userDto == null) return new ResponseInfo(success: false, message: "wrong request data");
            var newUser = mapper.Map<User>(userDto);

            var user = await repository.GetUserById(userDto.Id);
            if (user is null) return new ResponseInfo(success: false, message: "user not found");

            await repository.EditUser(newUser);

            return new ResponseInfo(success: true, message: $"user with id {newUser.Id} updated");
        }

        public async Task<ResponseInfo> ConfirmEmail(int userId, string code)
        {
            var emailConfirmation = await emailConfirmationRepository.GetEmailConfirmation(userId, code);

            if (emailConfirmation is null)
            {
                return new ResponseInfo(success: false, message: "not found");
            }

            await emailConfirmationRepository.RemoveEmailConfirmationById(emailConfirmation.Id);
            var user = await repository.GetUserById(userId);
            user.IsEmailConfirmed = true;
            await repository.EditUser(user);

            return new ResponseInfo(success: true, message: "email confirmed");
        }
    }
}
