using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Domain.DTOs.CouponDTO;

public class GetCouponDTO
{
    public int Id { get; set; }
        
    public int Discount { get; set; }
    
    public int UserId { get; set; }
}