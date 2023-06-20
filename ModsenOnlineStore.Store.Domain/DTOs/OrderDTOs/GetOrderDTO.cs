using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs
{
    public class GetOrderDTO
    {
        public int Id { get; set; }

        public string DeliveryAddress { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public bool Paid { get; set; } = false;
    }
}
