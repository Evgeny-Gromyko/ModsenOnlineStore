using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int ProductQuantity { get; set; } = 1;
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
