using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;
using ModsenOnlineStore.Login.Domain.Entities;
using ModsenOnlineStore.Common;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface ILoginService
    {
        Task<DataResponseInfo<string>> GetTokenAsync(LoginData data);

        Task<DataResponseInfo<List<User>>> GetAllUsersAsync();

        Task<DataResponseInfo<User>> GetUserByIdAsync(int id);

        Task<ResponseInfo> RegisterUserAsync(AddUserDTO user);

        Task<ResponseInfo> DeleteUserAsync(int id);

        Task<ResponseInfo> UpdateUserAsync(UpdateUserDTO user);

        Task<ResponseInfo> ConfirmEmailAsync(int userId, string code);
    }
}
