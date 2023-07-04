namespace ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;

public class GetOrderProductDTO
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    public int OrderId { get; set; }
    
    public int ProductQuantity { get; set; } = 1;
}
