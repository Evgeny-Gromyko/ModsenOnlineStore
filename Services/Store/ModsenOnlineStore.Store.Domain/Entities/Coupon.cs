using System.ComponentModel.DataAnnotations.Schema;

namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class Coupon
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]

        public decimal Discount { get; set; }
        
        public int UserId { get; set; }
    }
}
