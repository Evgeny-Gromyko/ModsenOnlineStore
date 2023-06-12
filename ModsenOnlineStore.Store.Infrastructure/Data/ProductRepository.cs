using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces;
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

        public async Task<Product> GetProductById(int id)
        {
            return await context.Products.AsNoTracking().FirstAsync(p => p.Id == id);
        }

        public async Task<List<Product>> AddProduct(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return await GetAllProducts();
        }

        public async Task<List<Product>> UpdateProduct(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return await GetAllProducts();
        }

        public async Task<List<Product>> RemoveProductById(int id)
        {
            var product = await context.Products.FirstAsync(p => p.Id == id);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return await GetAllProducts();
        }
    }
}
