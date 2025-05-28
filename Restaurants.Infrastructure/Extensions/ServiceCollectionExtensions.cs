using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {

            var connectionString = Configuration.GetConnectionString("RestaurantsDb");

            // Without Sensitive logging
            // Services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString));

            // With Sensitive logging
            Services.AddDbContext<RestaurantsDbContext>(options =>
                                options.UseSqlServer(connectionString)
                                 .EnableSensitiveDataLogging());

            Services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            Services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        }
    }
}
