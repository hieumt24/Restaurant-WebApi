using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants =
        [
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC (short for Kentucky Fried Chicken) is a fast-food restaurant chain that specializes in fried chicken. It was founded by Colonel Harland Sanders in 1930 and has since become one of the largest fast-food chains in the world.", 
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes = [
                    new()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 psc.)",
                        Price = 10.30M,
                    },
                    
                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 psc.)",
                        Price = 5.30M,
                    },
                    
                ],
                
                Address = new ()
                {
                    City = "London",
                    Street = "Oxford Street",
                    PostalCode = "W1D 1BS"
                }
            },
            new Restaurant()
            {
                Name = "MCDonald's",
                Category = "Fast Food",
                Description = "McDonald's is a global fast-food restaurant chain that is known for its hamburgers, fries, and breakfast items. Founded in 1940, it has become one of the largest and most recognizable fast-food brands in the world.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots Street",
                    PostalCode = "W1F 8RS"
                }
            }

        ];
        
        return restaurants;
    }
}