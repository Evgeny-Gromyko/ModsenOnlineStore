namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class ProductType
    {
        public int Id { get; set; }
        
        public string TypeName { get; set; }

        public List<Product> Products { get; set; }
    }
}
