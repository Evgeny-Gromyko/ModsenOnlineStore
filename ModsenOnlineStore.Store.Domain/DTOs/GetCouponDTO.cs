using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Domain.DTOs;

public class GetCouponDTO
{
    public int Id { get; set; }
        
    public int Discount { get; set; }
    
    public User User { get; set; }

    public int UserId { get; set; }
}