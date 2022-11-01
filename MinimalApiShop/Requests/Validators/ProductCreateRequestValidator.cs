using FluentValidation;

namespace MinimalApiShop.Requests.Validators;

public class ProductValidator : AbstractValidator<ProductCreateRequest>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name can not be empty!");

        RuleFor(p => p.Category)
            .IsInEnum()
            .WithMessage("Request is not in enum!");

        RuleFor(p => p.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity have to be grater than 0!");
    }
}
