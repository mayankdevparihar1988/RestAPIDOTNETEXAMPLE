namespace Restaurants.Domain.Entities
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string? Category { get; set; } = default!;
        public string? ImageUrl { get; set; } = default!;
        public bool IsAvailable { get; set; } = default!;

        public int? KiloCalories { get; set; } = default!;

        public int RestaurantId { get; set; } = default!;

    }
}