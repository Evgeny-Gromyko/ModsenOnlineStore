namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }
    }
}
