namespace Restaurants.Domain.Entities
{
    public class Address
    {
       
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string? State { get; set; } = default!;
        public string? ZipCode { get; set; } = default!;
        public string? Country { get; set; } = default!;
        public double? Latitude { get; set; } = default!;
        public double? Longitude { get; set; } = default!;
        public string? FormattedAddress { get; set; } = default!;
        public bool? IsDefault { get; set; } = default!;
        public bool? IsDeleted { get; set; } = default!;
    }
}