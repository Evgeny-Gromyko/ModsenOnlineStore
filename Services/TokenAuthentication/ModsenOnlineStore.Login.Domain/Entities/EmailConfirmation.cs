namespace ModsenOnlineStore.Login.Domain.Entities
{
    public class EmailConfirmation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Code { get; set; } = string.Empty;
    }
}
