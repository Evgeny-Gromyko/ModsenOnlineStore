using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Login.Application.Interfaces;
using ModsenOnlineStore.Login.Domain.Entities;

namespace ModsenOnlineStore.Login.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password) =>
            await context.Users.AsNoTracking().FirstOrDefaultAsync(p => (p.Email == email) && (p.Password == password));

        public async Task<List<User>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await context.Users.AsNoTracking().ToListAsync();

            if (pageNumber < 1)
            {
                return users;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            return users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<User?> GetUserByIdAsync(int id) =>
            await context.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RegisterUserAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);

            if (user is not null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task EditUserAsync(User user)
        {
            var prevUser = await GetUserByIdAsync(user.Id);

            if (prevUser is not null)
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
