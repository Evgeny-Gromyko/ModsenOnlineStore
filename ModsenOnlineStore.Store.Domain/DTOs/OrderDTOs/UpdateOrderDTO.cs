using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs
{
    public class UpdateOrderDTO
    {
        public int Id { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Paid { get; set; } = false;

    }
}
