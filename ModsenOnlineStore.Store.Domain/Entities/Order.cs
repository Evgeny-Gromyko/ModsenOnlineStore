using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPrice { get; set; } = 0;     }
}
