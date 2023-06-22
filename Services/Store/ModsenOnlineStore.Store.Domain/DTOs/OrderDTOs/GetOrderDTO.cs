using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;

public class GetOrderDTO
{
    public int Id { get; set; }
    
    public string DeliveryAddress { get; set; } = string.Empty;

    public decimal TotalPrice { get; set; };

    public List<Product> Products { get; set; } = new List<Domain.Entities.Product>();
    
    public bool Paid { get; set; } = false;
}