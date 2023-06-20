namespace ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;

public class AddProductToOrderDTO
{
    public int productId { get; set; }
    
    public int orderId { get; set; }
    
    public int quantity { get; set; } = 1;
}