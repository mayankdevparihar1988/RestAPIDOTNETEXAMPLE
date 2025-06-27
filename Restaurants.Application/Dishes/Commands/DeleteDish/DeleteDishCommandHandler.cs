using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand, bool>
    {
        private IDishesRepository _dishRepository;
        private ILogger<DeleteDishCommandHandler> _logger;

        public DeleteDishCommandHandler(IDishesRepository dishesRepository, ILogger<DeleteDishCommandHandler> logger)
        {
            _dishRepository = dishesRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DeleteDishCommand");

            int restaurantId = request._restaurantId;
            int dishId = request._dishId;

            try
            {
                await _dishRepository.DeleteAsync(restaurantId, dishId);
                _logger.LogInformation($"Dish with ID {dishId} from Restaurant {restaurantId} deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting Dish with ID {dishId} from Restaurant {restaurantId}.");
                return false;
            }
        }
    }
}
