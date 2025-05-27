using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(50).WithMessage("Category must not exceed 50 characters.");

            RuleFor(x => x.ContactEmail)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.ContactEmail))
                .WithMessage("ContactEmail must be a valid email address.");

            RuleFor(x => x.ContactNumber)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.ContactNumber))
                .WithMessage("ContactNumber must not exceed 20 characters.");

            RuleFor(x => x.City)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.City))
                .WithMessage("City must not exceed 100 characters.");

            RuleFor(x => x.Street)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Street))
                .WithMessage("Street must not exceed 100 characters.");

            RuleFor(x => x.PostalCode)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.PostalCode))
                .WithMessage("PostalCode must not exceed 20 characters.");
        }
    }
}
