using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
    {

        private readonly ILogger<CreateRestaurantCommandHandler> _logger;

        private readonly IRestaurantsRepository _restaurantsRepository;


        private readonly IMapper _mapper;




        public UpdateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository ?? throw new ArgumentNullException(nameof(restaurantsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating a restaurant with ID {Id}", request.Id);
            _logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant is null)
                return false;

            // Map the request to the restaurant entity
            var newEntity = _mapper.Map(request, restaurant);

            await _restaurantsRepository.Update(restaurant, newEntity);
            return true;



        }
    }
}
