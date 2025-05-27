using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository : IRestaurantsRepository
    {
        private readonly RestaurantsDbContext _dbContext;

        public RestaurantsRepository(RestaurantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(Restaurant entity)
        {
       
            _dbContext.Restaurants.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        
        }

        public async Task<Restaurant?> Delete(int id)
        {
            var restaurant = await _dbContext.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant == null)
            {
                return null;
            }

            _dbContext.Restaurants.Remove(restaurant);
            await _dbContext.SaveChangesAsync();
            return restaurant; // Ensure a value is returned in all code paths
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _dbContext.Restaurants.ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await _dbContext.Restaurants
                .Include(r=> r.Dishes)
                .FirstOrDefaultAsync(x => x.Id == id);
            return restaurant;
        }

        public async Task<bool?> Update(Restaurant entity, Restaurant newEntity)
        {
            var restaurant = _dbContext.Restaurants
                .FirstOrDefault(x => x.Id == entity.Id);

            if (restaurant is null)
            {
                return false;
            }

            restaurant.Name = newEntity.Name;
            restaurant.Description = newEntity.Description;
            restaurant.Category = newEntity.Category;
            restaurant.HasDelivery = newEntity.HasDelivery;
            restaurant.ContactEmail = newEntity.ContactEmail;
            restaurant.ContactNumber = newEntity.ContactNumber;
            restaurant.Address = newEntity.Address;
            await _dbContext.SaveChangesAsync();

            return true;

        }
    }

}
