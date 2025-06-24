using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    // IRequest is a marker interface for MediatR (GetAllRestaurantQuery)
    // It represents a request that can be handled by a handler
    // In this case, it represents a query to get all restaurants
    // and the response will be a collection of RestaurantDto objects
    // The handler is GetAllRestaurantQueryHandler
    // The handler implements IRequestHandler<GetAllRestaurantQuery, IEnumerable<RestaurantDto>>
    public class GetAllRestaurantQueryHandler : IRequestHandler<GetAllRestaurantQuery, IEnumerable<RestaurantDto>>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;

        private readonly IMapper _mapper;
        public GetAllRestaurantQueryHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository ?? throw new ArgumentNullException(nameof(restaurantsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _restaurantsRepository.GetAllAsync();

            // Use AutoMapper to map entities to DTOs
            var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            //var restaurantDtos = restaurants.Select(RestaurantDto.FromEntity).ToList();
            return restaurantDtos!;
        }
    }
}
