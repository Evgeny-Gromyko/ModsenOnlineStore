using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        
        public int Discount { get; set; }
        
        public User User { get; set; }
        
        public int UserId { get; set; }
    }
}
