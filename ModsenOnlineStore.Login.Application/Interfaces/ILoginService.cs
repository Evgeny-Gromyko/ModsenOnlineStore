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
        Task<ResponseInfo<string>> GetToken(LoginData data);
        Task<ResponseInfo<List<User>>> GetAllUsers();
        Task<ResponseInfo<User>> GetUserById(int id);
        Task<ResponseInfo<List<User>>> RegisterUser(AddUserDto user);
        Task<ResponseInfo<List<User>>> DeleteUser(int id);
        Task<ResponseInfo<User>> UpdateUser(UpdateUserDto user);
    }
}
