using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries;

namespace cleancodeprojectwebapi.Controllers
{
    [Route("api/resturant/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId,[FromBody] CreateDishCommand command)
        {
            

            command.RestaurantId = restaurantId;

            if (restaurantId==0)
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
    }
}
