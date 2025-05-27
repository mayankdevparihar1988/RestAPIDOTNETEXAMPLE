using MediatR;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{

    public class CreateRestaurantCommand : IRequest<int> // Fix: Ensure CreateRestaurantCommand implements IRequest<int>
    {
        public string Name { get; set; } = default!; // Ensure default value is set
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}