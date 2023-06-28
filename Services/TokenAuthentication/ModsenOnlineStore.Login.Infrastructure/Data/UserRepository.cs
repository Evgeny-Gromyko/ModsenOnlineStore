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

        public async Task<User?> AuthenticateUser(string email, string password) =>
            await context.Users.AsNoTracking().FirstOrDefaultAsync(p => (p.Email == email) && (p.Password == password));

        public async Task<List<User>> GetAllUsers() =>
            await context.Users.AsNoTracking().ToListAsync();

        public async Task<User?> GetUserById(int id) =>
            await context.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<User?> GetUserByEmail(string email)
        {
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RegisterUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await GetUserById(id);

            if (user is not null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task EditUser(User user)
        {
            var prevUser = await GetUserById(user.Id);

            if (prevUser is not null)
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
