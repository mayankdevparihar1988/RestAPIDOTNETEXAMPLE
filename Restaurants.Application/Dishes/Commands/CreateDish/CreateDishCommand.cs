using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    // This command is used to create a new dish in the system.
    // It implements IRequest<int> to return the ID of the created dish.
    public class CreateDishCommand : IRequest<int>
    {
        public int RestaurantId { get; set; } = default!; // The ID of the restaurant to which the dish belongs.
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string? Category { get; set; } = default!;
        public string? ImageUrl { get; set; } = default!;
        public bool IsAvailable { get; set; } = true;
        public int? KiloCalories { get; set; } = default!;

    }
}
