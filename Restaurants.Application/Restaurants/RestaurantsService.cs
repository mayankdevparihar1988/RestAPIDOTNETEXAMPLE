using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService : IRestaurantsService
    {

        private readonly IRestaurantsRepository _restaurantsRepository;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public RestaurantsService(IRestaurantsRepository restaurantsRepository, IMapper mapper, ILogger<RestaurantsService> logger)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {

            var restaurants =  await _restaurantsRepository.GetAllAsync();

            // Use AutoMapper to map entities to DTOs
            var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            //var restaurantDtos = restaurants.Select(RestaurantDto.FromEntity).ToList();
            return restaurantDtos!;

        }
        public async Task<RestaurantDto?> GetById(int id)
        {
            
            var restaurant = await _restaurantsRepository.GetByIdAsync(id);
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);  // RestaurantDto.FromEntity(restaurant);
            return restaurantDto;

        }

        public async Task<int> Create(CreateRestaurantDto dto)
        {
            _logger.LogInformation("Creating a new restaurant");

            var restaurant = _mapper.Map<Restaurant>(dto);

            int id = await _restaurantsRepository.Create(restaurant);
            return id;
        }
    }
}
