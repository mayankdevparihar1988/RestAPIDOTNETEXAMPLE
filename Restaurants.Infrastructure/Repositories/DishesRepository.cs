using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishesRepository(RestaurantsDbContext restaurantsDbContext, ILogger<DishesRepository> logger) : IDishesRepository
    {
      
        public async Task<int> CreateAsync(Dish dish)
        {
            if (dish == null) throw new ArgumentNullException(nameof(dish));

            restaurantsDbContext.Dishes.Add(dish);
            await restaurantsDbContext.SaveChangesAsync();
            logger.LogInformation($"Dish with ID {dish.Id} created successfully.");
            return dish.Id;
        }

        public async Task DeleteAsync(int restaurantId, int dishId)
        {
            if (restaurantId == 0 || dishId == 0)
            {
                throw new ArgumentException("Restaurant ID and Dish ID must be non-zero.");
            }

            var dish = await restaurantsDbContext.Dishes
                .FirstOrDefaultAsync(d => d.RestaurantId == restaurantId && d.Id == dishId);

            if (dish == null)
            {
                throw new InvalidOperationException($"Dish with ID {dishId} for Restaurant {restaurantId} not found.");
            }

            restaurantsDbContext.Dishes.Remove(dish);
            await restaurantsDbContext.SaveChangesAsync();
            logger.LogInformation($"Dish with ID {dishId} for Restaurant {restaurantId} deleted successfully.");
        }

        public Task<IEnumerable<Dish>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Dish?> GetByIdAsync(int restaurentId, int dishId)
        {
            if (restaurentId == 0 || dishId == 0)
            {
                throw new ArgumentException("Restaurant ID and Dish ID must be non-zero.");
            }

            return await restaurantsDbContext.Dishes
                .FirstOrDefaultAsync(d => d.RestaurantId == restaurentId && d.Id == dishId);
        }

        public Task<IEnumerable<Dish>> GetByRestaurantIdAsync(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Dish dish)
        {
            throw new NotImplementedException();
        }
    }
}
