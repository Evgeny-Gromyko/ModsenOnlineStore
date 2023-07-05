namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        
        public decimal Discount { get; set; }
        
        public int UserId { get; set; }
    }
}
