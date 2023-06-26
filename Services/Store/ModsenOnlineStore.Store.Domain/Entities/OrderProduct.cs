namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        
        public int ProductQuantity { get; set; } = 1;
        
        public Order Order { get; set; }

        public int OrderId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}
