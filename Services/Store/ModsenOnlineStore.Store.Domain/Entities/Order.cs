namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string DeliveryAddress { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public bool Paid { get; set; } = false;

        public string? PaymentConfirmationCode { get; set; }

        public int UserId { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new();
    }
}
