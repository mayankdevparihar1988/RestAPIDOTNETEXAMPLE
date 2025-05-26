
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder : IRestaurantSeeder
    {
        RestaurantsDbContext dbContext;
        public RestaurantSeeder(RestaurantsDbContext restaurantsDbContext)
        {
            dbContext = restaurantsDbContext;

        }

        public async Task Seed()
        {
            // Check if dbContext can connect the database

            if (!await dbContext.Database.CanConnectAsync())
            {
                throw new Exception("Cannot connect to the database");
            }

            if (!dbContext.Restaurants.Any())
            {
                var restaurant = new Restaurant
                {
                    Name = "Test Restaurant",
                    Description = "Test Description",
                    Category = "VEG",
                    Address = new Address
                    {
                        Street = "123 Test St",
                        City = "Test City",
                        State = "TS",
                        ZipCode = "12345"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Test Dish 1",
                            Description = "Test Dish 1 Description",
                            Price = 10.99m
                        },
                        new Dish
                        {
                            Name = "Test Dish 2",
                            Description = "Test Dish 2 Description",
                            Price = 12.99m
                        }
                    }
                };
                dbContext.Restaurants.Add(restaurant);
                await dbContext.SaveChangesAsync();
            }
        }



    }
}
