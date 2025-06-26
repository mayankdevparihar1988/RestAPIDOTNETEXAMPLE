using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator() { 
        
            RuleFor(dish => dish.Name)
                .NotEmpty().WithMessage("Dish name is required.")
                .MaximumLength(100).WithMessage("Dish name must not exceed 100 characters.");
            RuleFor(dish => dish.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.")
                .LessThanOrEqualTo(100).WithMessage("Price must not exceed 100.");
            RuleFor(dish => dish.KiloCalories)
                .GreaterThanOrEqualTo(0).WithMessage("KiloCalories must be greater than or equal to zero.")
                .LessThanOrEqualTo(10000).WithMessage("KiloCalories must not exceed 10000.");
        }

    }
}
