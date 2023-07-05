using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data
{
    public class ProductRepository : PagedRepository<Product>, IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            var products = await context.Products.AsNoTracking().ToListAsync();

            return ToPagedList(products, pageNumber, pageSize);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task RemoveProductByIdAsync(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is not null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllProductsByProductTypeIdAsync(int id, int pageNumber, int pageSize)
        {
            var products = await context.Products.AsNoTracking().Where(p => p.ProductTypeId == id).ToListAsync();

            return ToPagedList(products, pageNumber, pageSize);
        }
    }
}
