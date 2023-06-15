namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public List<Comment> Comments { get; set; } = new();
    }
}
