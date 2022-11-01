﻿using FluentValidation;

namespace MinimalApiShop.Requests.Validators;

public class ProductChangeAtributeRequestValidator : AbstractValidator<ProductChangeAtribute>
{
    public ProductChangeAtributeRequestValidator()
    {
        RuleFor(x => x.Atribute)
            .MinimumLength(5)
            .WithMessage("Minimum length of atribute is 5!");
    }
}
