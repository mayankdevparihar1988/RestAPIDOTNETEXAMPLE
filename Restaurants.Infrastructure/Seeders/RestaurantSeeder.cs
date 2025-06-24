
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
                var restaurants = new List<Restaurant>
                {
                    new Restaurant
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
                    },
                    new Restaurant
                    {
                        Name = "Ocean's Delight",
                        Description = "Seafood and grill",
                        Category = "SEAFOOD",
                        Address = new Address
                        {
                            Street = "456 Ocean Ave",
                            City = "Seaside",
                            State = "CA",
                            ZipCode = "90210"
                        },
                        Dishes = new List<Dish>
                        {
                            new Dish
                            {
                                Name = "Grilled Salmon",
                                Description = "Fresh salmon grilled to perfection",
                                Price = 18.50m
                            },
                            new Dish
                            {
                                Name = "Shrimp Tacos",
                                Description = "Spicy shrimp with fresh toppings",
                                Price = 14.00m
                            }
                        }
                    },
                    new Restaurant
                    {
                        Name = "Pasta Palace",
                        Description = "Authentic Italian cuisine",
                        Category = "ITALIAN",
                        Address = new Address
                        {
                            Street = "789 Pasta Rd",
                            City = "Little Italy",
                            State = "NY",
                            ZipCode = "10012"
                        },
                        Dishes = new List<Dish>
                        {
                            new Dish
                            {
                                Name = "Spaghetti Carbonara",
                                Description = "Classic carbonara with pancetta",
                                Price = 13.75m
                            },
                            new Dish
                            {
                                Name = "Margherita Pizza",
                                Description = "Wood-fired pizza with fresh mozzarella",
                                Price = 11.25m
                            }
                        }
                    }
                };

                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }
        }



    }
}
