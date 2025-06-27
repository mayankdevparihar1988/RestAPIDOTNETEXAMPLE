using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries;
using Restaurants.Application.Dishes.Queries.GetDishById;

namespace cleancodeprojectwebapi.Controllers
{
    [Route("api/resturant/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishCommand command)
        {


            command.RestaurantId = restaurantId;

            if (restaurantId == 0)
            {
                return BadRequest("Restaurant ID in the route does not match the Restaurant ID in the command.");
            }

            int newDishId = await mediator.Send(command);
            return CreatedAtAction(nameof(CreateDish), new { restaurantId = restaurantId, dishId = newDishId }, command);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishForRestaurant([FromRoute] int restaurantId)
        {


            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));

            return Ok(dishes);

        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetDishById([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdQuery(restaurantId, dishId));
            if (dish == null)
            {
                return NotFound($"Dish with ID {dishId} not found for restaurant with ID {restaurantId}.");
            }
            return Ok(dish);
        }

        [HttpDelete("{dishId}")]
        public async Task<ActionResult<bool>> DeleteDish([FromRoute] int restaurantId, [FromRoute] int dishId)
        { 

            var isDeleted = await mediator.Send( new DeleteDishCommand(restaurantId,dishId));

            return Ok(isDeleted);
        
        }

    }
}
