using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishById
{
    public class GetDishByIdQueryHandler(IDishesRepository dishesRepository, IMapper mapper, ILogger<GetDishByIdQueryHandler> logger) : IRequestHandler<GetDishByIdQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetDishByIdQuery for RestaurantId: {RestaurantId}, DishId: {DishId}", request.RestaurantId, request.DishId);
            var dish = await dishesRepository.GetByIdAsync(request.RestaurantId, request.DishId);
            if (dish == null)
            {
                logger.LogWarning("Dish with ID {DishId} not found for Restaurant ID {RestaurantId}.", request.DishId, request.RestaurantId);
                throw new NotFoundException("Dish not found for the given Restaurant ID.");
            }
            var dishDto = mapper.Map<DishDto>(dish);

            logger.LogInformation("Successfully retrieved Dish with ID {DishId} for Restaurant ID {RestaurantId}.", request.DishId, request.RestaurantId);
            return dishDto;
        }
    }
}
