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
        Task<List<User>> GetAllUsersAsync();
        
        Task<User> GetUserByIdAsync(int id);
        
        Task<User> AuthenticateUserAsync(string email, string password);
        
        Task<User> RegisterUserAsync(User user);
        
        Task<User> DeleteUserAsync(int id);
        
        Task<User> EditUserAsync(User user);
    }
}
