using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data;

public class DbInitializer
{
    public static async Task SeedData(DataContext context)
    {
        var firstUser = new User()
        {
        };

        var Ivan = new User()
        {

        };
        
        if (!context.Users.Any()) {
            context.Users.AddRange(new User[] { firstUser, Ivan});
        }


        if (!context.ProductTypes.Any()) {
            var firstPT = new Domain.Entities.ProductType()
            {
                TypeName = "PT 1",
            };
            var secondPT = new Domain.Entities.ProductType()
            {
                TypeName = "PT 2",
            };
            
            var thirdPT = new Domain.Entities.ProductType()
            {
                TypeName = "PT 3",
            };

            context.ProductTypes.AddRange(new Domain.Entities.ProductType[] { firstPT, secondPT, thirdPT });
        }
        
        if (!context.Orders.Any()) {
            var firstOrder = new Domain.Entities.Order()
            {
                User = firstUser,
            };
            var secondO = new Domain.Entities.Order()
            {
                User = Ivan,
            };

            context.Orders.AddRange(new Domain.Entities.Order[] { firstOrder, secondO });
        }
        
        var milk = new Product() {
            Price = 1
        };
        var beef = new Product() {
            Price = 2

        };
        if (!context.Products.Any()) {
            context.Products.AddRange(new Product[]
                { beef, milk }
            );
        }
            
        await context.SaveChangesAsync();
    }
}