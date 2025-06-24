using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    // IRequest is a marker interface for MediatR
    // It represents a request that can be handled by a handler
    // In this case, it represents a query to get all restaurants
    // and the response will be a collection of RestaurantDto objects
    public class GetAllRestaurantQuery: IRequest<IEnumerable<RestaurantDto>>
    {
    }
}
