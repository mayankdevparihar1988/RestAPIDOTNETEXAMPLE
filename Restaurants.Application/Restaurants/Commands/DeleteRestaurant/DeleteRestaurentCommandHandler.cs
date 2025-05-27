using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurentCommandHandler : IRequestHandler<DeleteRestaurantCommand, RestaurantDto>
    {
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;

        private readonly IRestaurantsRepository _restaurantsRepository;

        private readonly IMapper _mapper;

        public DeleteRestaurentCommandHandler(IRestaurantsRepository restaurantsRepository, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository ?? throw new ArgumentNullException(nameof(restaurantsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<RestaurantDto> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting a restaurant with ID {Id}", request.Id);
            // Retrieve the restaurant by ID
            var restaurant = await _restaurantsRepository.Delete(request.Id);
            if (restaurant == null)
            {
                _logger.LogWarning("Restaurant with ID {Id} not found", request.Id);
                throw new KeyNotFoundException($"Restaurant with ID {request.Id} not found.");
            }
            
           return _mapper.Map<RestaurantDto>(restaurant);

        }
    }
}
