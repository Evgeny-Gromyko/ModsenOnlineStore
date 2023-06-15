using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;

public class GetOrderDTO
{
    public int Id { get; set; }
    
    public decimal TotalPrice { get; set; } = 0;
    
    public string DeliveryAddress { get; set; } = "";
    
    public List<Product> Products { get; set; } = new List<Domain.Entities.Product>();
    
    public User User { get; set; }
    
    public bool Paid { get; set; } = false;
}