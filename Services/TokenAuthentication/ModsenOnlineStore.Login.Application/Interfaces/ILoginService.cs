using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;
using ModsenOnlineStore.Login.Domain.Entities;
using ModsenOnlineStore.Common;
using Microsoft.AspNetCore.Http;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface ILoginService
    {
        Task<DataResponseInfo<string>> GetTokenAsync(LoginData data);

        Task<DataResponseInfo<List<User>>> GetAllUsersAsync(int pageNumber, int pageSize);

        Task<DataResponseInfo<User>> GetUserByIdAsync(int id);
        
        Task<ResponseInfo> RegisterUserAsync(AddUserDTO user, HttpRequest httpRequest);

        Task<ResponseInfo> DeleteUserAsync(int id);

        Task<ResponseInfo> UpdateUserAsync(UpdateUserDTO user);

        Task<ResponseInfo> ConfirmEmailAsync(int userId, string code);
    }
}
