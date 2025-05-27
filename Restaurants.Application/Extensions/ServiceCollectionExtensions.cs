
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection Services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

            Services.AddScoped<IRestaurantsService, RestaurantsService>();
            Services.AddAutoMapper(applicationAssembly);

            // Ensure the correct FluentValidation package is referenced
            // AddFluentValidationAutoValidation is part of FluentValidation.AspNetCore
            Services.AddValidatorsFromAssemblyContaining<CreateRestaurantCommandValidator>();

            Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        }
    }
}
