using ModsenOnlineStore.Store.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        //public ProductType ProductType { get; set; } = new();
        public int ProductTypeId { get; set; }
    }
}
