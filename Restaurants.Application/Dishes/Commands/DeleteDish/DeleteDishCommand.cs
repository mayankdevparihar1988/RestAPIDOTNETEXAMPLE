using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommand: IRequest<bool>
    {
        public int _restaurantId { get; set; }
        public int _dishId { get; set; }
        public DeleteDishCommand(int restaurantId, int dishId) {
          
            _dishId = dishId;
            _restaurantId = restaurantId; 

        }
    }
}
