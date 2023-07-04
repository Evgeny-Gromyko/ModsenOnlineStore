namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface IEncryptionService
    {
        string HashPassword(string password);
    }
}
