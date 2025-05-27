using Azure.Core;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;
using FluentValidation;
using MediatR;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Microsoft.AspNetCore.Components.Forms;

namespace cleancodeprojectwebapi.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
       // private readonly IRestaurantsService restaurantsService;
        private readonly IValidator<CreateRestaurantCommand> _validator; // Corrected type to match the generic parameter
        private readonly IMediator _mediator;

        public RestaurantsController(IMediator mediator, IValidator<CreateRestaurantCommand> validator)
        {
            // this.restaurantsService = restaurantsService ?? throw new ArgumentNullException(nameof(restaurantsService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); // Added null check for mediator
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); // Added null check for validator
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        //{
        //    var restaurants = await restaurantsService.GetAllRestaurants();
        //    return Ok(restaurants);
        //}

        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<RestaurantDto>> GetById([FromRoute] int id)
        //{
        //    var restaurant = await restaurantsService.GetById(id);
        //    if (restaurant is null)
        //        return NotFound();

        //    return Ok(restaurant);
        //}

        [HttpPost]
        public async Task<ActionResult<int>> CreateRestaurant([FromBody] CreateRestaurantCommand command, IValidator<CreateRestaurantCommand> validator)
        {
            var validationResult = await _validator.ValidateAsync(command); // Validation logic corrected
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary()); // Fixed Results.ValidationProblem to ValidationProblem
            }
             int id = await _mediator.Send(command);
            return Ok(id);// Return the created restaurant's ID
        }

      
    }
}
