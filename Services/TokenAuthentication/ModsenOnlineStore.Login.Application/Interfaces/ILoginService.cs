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
        Task<DataResponseInfo<string>> GetToken(LoginData data);
        Task<DataResponseInfo<List<User>>> GetAllUsers();
        Task<DataResponseInfo<User>> GetUserById(int id);
        Task<DataResponseInfo<List<User>>> RegisterUser(AddUserDto user);
        Task<DataResponseInfo<List<User>>> DeleteUser(int id);
        Task<ResponseInfo> NewPayment(int id, decimal money);
        Task<DataResponseInfo<User>> UpdateUser(UpdateUserDto user);
    }
}
