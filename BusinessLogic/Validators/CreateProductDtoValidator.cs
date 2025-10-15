﻿using BusinessLogic.DTOs;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Title)
              .NotEmpty()
              .MinimumLength(3)
              .Matches("^[A-Z].*").WithMessage("{PropertyName} must starts with uppercase letter.");

            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);

            RuleFor(x => x.CategoryId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .MinimumLength(10)
                .MaximumLength(3000);
        }
    }
}
