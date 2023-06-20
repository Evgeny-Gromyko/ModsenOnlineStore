using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(DataContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Products.Add(new Product());
            await context.SaveChangesAsync();
        }
    }
}
