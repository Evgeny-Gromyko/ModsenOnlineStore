using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public async Task<DataResponseInfo<string>> GetTokenAsync(LoginData data)
        {
            var user = await repository.AuthenticateUserAsync(data.Email, data.Password);

            if (user is null) return new DataResponseInfo<string>(data: null, success: false, message: "user is not found");

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

        public async Task<DataResponseInfo<List<User>>> GetAllUsersAsync() =>
            new DataResponseInfo<List<User>>(data: await repository.GetAllUsersAsync(), success: true, message: "all users");

        public async Task<DataResponseInfo<User>> GetUserByIdAsync(int id)
        {
            var user = await repository.GetUserByIdAsync(id);

            if (user is null) return new DataResponseInfo<User>(data: null, success: false, message: $"user with id {id} not found");

            return new DataResponseInfo<User>(data: user, success: true, message: $"user with id {user.Id}");
        }

        public async Task<ResponseInfo> RegisterUserAsync(AddUserDTO userDto)
        {
            if (userDto is null) return new ResponseInfo(success: false, message: "wrong request data");

            var user = await repository.GetUserByEmailAsync(userDto.Email);

            if (user is not null)
            {
                return new ResponseInfo(success: false, message: "this email is already in use");
            }

            var newUser = mapper.Map<User>(userDto);

            await repository.RegisterUserAsync(newUser);

            user = await repository.GetUserByEmailAsync(newUser.Email);

            var emailConfirmation = new EmailConfirmation
            {
                UserId = user!.Id,
                Code = Guid.NewGuid().ToString()
            };

            await emailConfirmationRepository.AddEmailConfirmationAsync(emailConfirmation);

            var message = $"{user.Email} {user.Id} {emailConfirmation.Code}";
            rabbitMQMessagingService.PublishMessage("email-confirmation", message);

            return new ResponseInfo(success: true, message: $"user with id {user.Id} registered");
        }

        public async Task<ResponseInfo> DeleteUserAsync(int id)
        {
            var user = await repository.GetUserByIdAsync(id);

            if (user is null) return new ResponseInfo(success: false, message: $"user with id {id} not found");

            await repository.DeleteUserAsync(id);

            return new ResponseInfo(success: true, message: $"user with id {id} deleted");
        }

        public async Task<ResponseInfo> UpdateUserAsync(UpdateUserDTO userDto)
        {
            if (userDto == null) return new ResponseInfo(success: false, message: "wrong request data");
            var newUser = mapper.Map<User>(userDto);

            var user = await repository.GetUserByIdAsync(userDto.Id);
            if (user is null) return new ResponseInfo(success: false, message: "user with id {newUser.Id} not found");

            await repository.EditUserAsync(newUser);

            return new ResponseInfo(success: true, message: $"user with id {newUser.Id} updated");
        }

        public async Task<ResponseInfo> ConfirmEmailAsync(int userId, string code)
        {
            var emailConfirmation = await emailConfirmationRepository.GetEmailConfirmationAsync(userId, code);

            if (emailConfirmation is null)
            {
                return new ResponseInfo(success: false, message: "not found");
            }

            await emailConfirmationRepository.RemoveEmailConfirmationByIdAsync(emailConfirmation.Id);
            var user = await repository.GetUserByIdAsync(userId);
            user.IsEmailConfirmed = true;
            await repository.EditUserAsync(user);

            return new ResponseInfo(success: true, message: "email confirmed");
        }
    }
}
