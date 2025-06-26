using AutoMapper;
using MediatR;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries
{
    internal class GetDishesForRestaurantQueryHandler : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        private readonly IRestaurantsRepository _restaurantDishesRepository;
        private readonly IMapper _mapper;
        public GetDishesForRestaurantQueryHandler(IRestaurantsRepository restaurantDishesRepository, IMapper mapper)
        {
            _restaurantDishesRepository = restaurantDishesRepository ?? throw new ArgumentNullException(nameof(IRestaurantsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantDishesRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException($"Restaurant with ID {request.RestaurantId} not found.");
            }
            var dishes = _mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

            return dishes;


        }
    }
}
