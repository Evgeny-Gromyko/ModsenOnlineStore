using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class OrderPaymentConfirmation
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string Code { get; set; } = string.Empty;
    }
}
