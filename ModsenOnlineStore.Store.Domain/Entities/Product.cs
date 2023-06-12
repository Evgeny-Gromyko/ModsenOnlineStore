using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductTypeId { get; set; }
        public int CouponId { get; set; }

        public Product(string name, string description, decimal price, int quantity, int productTypeId, int couponId)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            ProductTypeId = productTypeId;
            CouponId = couponId;
        }
    }
}
