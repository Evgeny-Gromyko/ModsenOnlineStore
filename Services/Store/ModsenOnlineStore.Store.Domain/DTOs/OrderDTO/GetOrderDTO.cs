using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Domain.DTOs.OrderDTO;

public class GetOrderDTO
{
    public int Id { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public string DeliveryAddress { get; set; } = "";
    
    public List<Product> Products { get; set; } = new List<Domain.Entities.Product>();
    
    public bool Paid { get; set; } = false;
}