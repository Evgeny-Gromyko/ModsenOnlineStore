using ModsenOnlineStore.Login.Domain.Entities;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync(int pageNumber, int pageSize);

        Task<User?> GetUserByIdAsync(int id);

        Task<User?> GetUserByEmailAsync(string email);

        Task<User?> AuthenticateUserAsync(string email, string password);

        Task RegisterUserAsync(User user);

        Task DeleteUserAsync(int id);

        Task EditUserAsync(User user);
    }
}
