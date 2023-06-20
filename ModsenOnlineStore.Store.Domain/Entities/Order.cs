using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string DeliveryAddress { get; set; } = string.Empty;

        public User User { get; set; }

        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public bool Paid { get; set; } = false;
    }
}
