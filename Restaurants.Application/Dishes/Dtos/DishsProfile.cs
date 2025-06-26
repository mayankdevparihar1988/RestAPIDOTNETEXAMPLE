

using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos
{
    public class DishsProfile: Profile
    {
        public DishsProfile() {

            CreateMap<CreateDishCommand, Dish>();
            // Create a mapping configuration between the Dish entity and the DishDto.
            // This will automatically map properties with the same name between the two types.

            CreateMap<Dish, DishDto>();

        }
    }
}
