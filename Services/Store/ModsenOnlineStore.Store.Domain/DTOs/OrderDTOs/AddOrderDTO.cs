namespace ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public string DeliveryAddress { get; set; } = string.Empty;

        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public string? PaymentConfirmationCode { get; set; }
    }
}
