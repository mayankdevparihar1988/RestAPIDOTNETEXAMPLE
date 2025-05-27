using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;

        private readonly IRestaurantsRepository _restaurantsRepository;

        private readonly IMapper _mapper;

        public CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository ?? throw new ArgumentNullException(nameof(restaurantsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new restaurant");

            var restaurant = _mapper.Map<Restaurant>(request);

            int id = await _restaurantsRepository.Create(restaurant);
            return id;
        }
    }

}
