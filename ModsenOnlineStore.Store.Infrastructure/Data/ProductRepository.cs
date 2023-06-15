using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProduct(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task RemoveProductById(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is not null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }
    }
}
