namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }

        public ProductType ProductType { get; set; }

        public int ProductTypeId { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new();
      
        public List<Comment> Comments { get; set; } = new();
    }
}
