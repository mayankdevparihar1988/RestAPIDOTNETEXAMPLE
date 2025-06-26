using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository, IMapper mapper
        ) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            // This method is responsible for handling the creation of a new dish.
            logger.LogInformation("Creating a new dish: {@DishRequest}", request);

            if(request == null)
            {
                logger.LogError("CreateDishCommand request is null.");
                throw new BadRequestException("Request cannot be null");
            }

            var restaurantId = request.RestaurantId;


            var restaurant = await restaurantsRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
            {
                logger.LogError("Restaurant with ID {RestaurantId} not found.", request.RestaurantId);
                throw new NotFoundException("Resturant not found");
            
            }

            var dish = mapper.Map<Dish>(request);

            var dishId = await dishesRepository.CreateAsync(dish);

            return dishId;


        }
    }
}
