using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Login.Application.Interfaces;
using ModsenOnlineStore.Login.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Login.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> AuthenticateUserAsync(string email, string password) =>
            await context.Users.AsNoTracking().FirstOrDefaultAsync(p => (p.Email == email) && (p.Password == password));

        public async Task<List<User>> GetAllUsersAsync() =>
            await context.Users.AsNoTracking().ToListAsync();


        public async Task<User> GetUserByIdAsync(int id) =>
            await context.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<User> RegisterUserAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            
            if (user is null) return null;

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            
            return user;
        }

        public async Task<User> EditUserAsync(User newUser)
        {
            var prevUser = await GetUserByIdAsync(newUser.Id);
            
            if (prevUser is null) return null;

            context.Users.Update(newUser);
            await context.SaveChangesAsync();

            return newUser;
        }
    }
}
