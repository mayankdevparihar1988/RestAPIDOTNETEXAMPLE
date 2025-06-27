using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> CreateAsync(Dish dish);
        Task<Dish?> GetByIdAsync(int restaurentId, int dishId);
        Task<IEnumerable<Dish>> GetAllAsync();
        Task<IEnumerable<Dish>> GetByRestaurantIdAsync(int restaurantId);
        Task UpdateAsync(Dish dish);
        Task DeleteAsync(int restuarntId, int dishId);
    
    }
}
