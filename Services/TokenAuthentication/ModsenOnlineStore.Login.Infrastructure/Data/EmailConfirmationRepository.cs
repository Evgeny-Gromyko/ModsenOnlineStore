using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Login.Application.Interfaces;
using ModsenOnlineStore.Login.Domain.Entities;

namespace ModsenOnlineStore.Login.Infrastructure.Data
{
    public class EmailConfirmationRepository : IEmailConfirmationRepository
    {
        private readonly DataContext context;

        public EmailConfirmationRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<EmailConfirmation?> GetEmailConfirmation(int userId, string code)
        {
            var emailConfirmations = context.EmailConfirmations.AsNoTracking();
            var emailConfirmation = await emailConfirmations.FirstOrDefaultAsync(ec => ec.UserId == userId && ec.Code == code);

            return emailConfirmation;
        }

        public async Task AddEmailConfirmation(EmailConfirmation emailConfirmation)
        {
            context.EmailConfirmations.Add(emailConfirmation);
            await context.SaveChangesAsync();
        }

        public async Task RemoveEmailConfirmationById(int id)
        {
            var emailConfirmations = context.EmailConfirmations.AsNoTracking();
            var emailConfirmation = await emailConfirmations.FirstOrDefaultAsync(ec => ec.Id == id);

            if (emailConfirmation is not null)
            {
                context.EmailConfirmations.Remove(emailConfirmation);
                await context.SaveChangesAsync();
            }
        }
    }
}
