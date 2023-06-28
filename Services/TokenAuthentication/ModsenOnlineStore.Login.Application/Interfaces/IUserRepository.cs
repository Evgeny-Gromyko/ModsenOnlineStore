using ModsenOnlineStore.Login.Domain.Entities;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User?> GetUserByEmail(string email);

        Task<User?> AuthenticateUser(string email, string password);

        Task RegisterUser(User user);

        Task DeleteUser(int id);

        Task EditUser(User user);
    }
}
