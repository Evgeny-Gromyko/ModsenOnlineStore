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

        public string DeliveryAddress { get; set; } = string.Empty;

        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public bool Paid { get; set; } = false;
    }
}
