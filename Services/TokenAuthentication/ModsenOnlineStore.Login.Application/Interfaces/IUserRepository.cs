using ModsenOnlineStore.Login.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> AuthenticateUser(string email, string password);
        Task<List<User>> RegisterUser(User user);
        Task<List<User>> DeleteUser(int id);
        Task<User> EditUser(User user);
    }
}
