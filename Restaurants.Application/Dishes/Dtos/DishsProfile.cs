

using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos
{
    public class DishsProfile: Profile
    {
        public DishsProfile() {

            CreateMap<Dish, DishDto>();

        }
    }
}
