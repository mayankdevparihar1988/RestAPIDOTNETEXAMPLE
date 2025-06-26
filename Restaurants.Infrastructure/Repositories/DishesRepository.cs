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

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dish>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Dish?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
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
