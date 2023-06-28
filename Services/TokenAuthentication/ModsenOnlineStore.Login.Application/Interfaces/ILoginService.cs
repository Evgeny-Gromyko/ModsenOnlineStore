using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;
using ModsenOnlineStore.Login.Domain.Entities;
using ModsenOnlineStore.Common;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface ILoginService
    {
        Task<DataResponseInfo<string>> GetToken(LoginData data);

        Task<DataResponseInfo<List<User>>> GetAllUsers();

        Task<DataResponseInfo<User>> GetUserById(int id);

        Task<ResponseInfo> RegisterUser(AddUserDto user);

        Task<ResponseInfo> DeleteUser(int id);

        Task<ResponseInfo> UpdateUser(UpdateUserDto user);

        Task<ResponseInfo> ConfirmEmail(int userId, string code);
    }
}
