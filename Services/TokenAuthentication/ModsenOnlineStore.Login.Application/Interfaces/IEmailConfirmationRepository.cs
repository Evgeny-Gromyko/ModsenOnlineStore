using ModsenOnlineStore.Login.Domain.Entities;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface IEmailConfirmationRepository
    {
        Task<EmailConfirmation?> GetEmailConfirmation(int userId, string code);

        Task AddEmailConfirmation(EmailConfirmation emailConfirmation);

        Task RemoveEmailConfirmationById(int id);
    }
}
