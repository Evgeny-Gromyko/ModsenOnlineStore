namespace ModsenOnlineStore.Store.Domain.DTOs;

public class AddOrderProductDTO
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
}