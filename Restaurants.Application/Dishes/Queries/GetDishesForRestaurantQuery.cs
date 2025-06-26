using MediatR;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries
{
    public class GetDishesForRestaurantQuery : IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; set; }

        public GetDishesForRestaurantQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

    }
}
