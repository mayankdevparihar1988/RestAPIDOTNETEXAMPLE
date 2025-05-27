using Azure.Core;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;
using FluentValidation;
using Restaurants.Application.Restaurants.Validators;
using MediatR;

namespace cleancodeprojectwebapi.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsService restaurantsService;
        private readonly IValidator<CreateRestaurantDto> _validator; // Corrected type to match the generic parameter

        public RestaurantsController(IRestaurantsService restaurantsService, IValidator<CreateRestaurantDto> validator)
        {
            this.restaurantsService = restaurantsService ?? throw new ArgumentNullException(nameof(restaurantsService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); // Added null check for validator
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            var restaurants = await restaurantsService.GetAllRestaurants();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RestaurantDto>> GetById([FromRoute] int id)
        {
            var restaurant = await restaurantsService.GetById(id);
            if (restaurant is null)
                return NotFound();

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto, IValidator<CreateRestaurantDto> validator)
        {
            var validationResult = await _validator.ValidateAsync(createRestaurantDto); // Validation logic corrected
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary()); // Fixed Results.ValidationProblem to ValidationProblem
            }
            int id = await restaurantsService.Create(createRestaurantDto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        private IActionResult ValidationProblem(IDictionary<string, string[]> dictionary)
        {
            throw new NotImplementedException();
        }
    }
}
