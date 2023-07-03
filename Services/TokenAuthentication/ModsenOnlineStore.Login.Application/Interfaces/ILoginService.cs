using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;
using ModsenOnlineStore.Login.Domain.Entities;
using ModsenOnlineStore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface ILoginService
    {
        Task<DataResponseInfo<string>> GetTokenAsync(LoginData data);
        
        Task<DataResponseInfo<List<User>>> GetAllUsersAsync();
        
        Task<DataResponseInfo<User>> GetUserByIdAsync(int id);
        
        Task<ResponseInfo> RegisterUserAsync(AddUserDto user);
        
        Task<ResponseInfo> DeleteUserAsync(int id);
        
        Task<ResponseInfo> UpdateUserAsync(UpdateUserDto user);
    }
}
