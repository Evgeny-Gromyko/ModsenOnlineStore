using ModsenOnlineStore.Login.Domain.Entities;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface IEmailConfirmationRepository
    {
        Task<EmailConfirmation?> GetEmailConfirmationAsync(int userId, string code);

        Task AddEmailConfirmationAsync(EmailConfirmation emailConfirmation);

        Task RemoveEmailConfirmationByIdAsync(int id);
    }
}
