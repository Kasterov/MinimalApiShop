using FluentValidation;

namespace MinimalApiShop.Requests.Validators;

public class ProductaddQuantityRequestValidator : AbstractValidator<ProductAddQuantityRequest>
{
    public ProductaddQuantityRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).
            WithMessage("Quantity have to be grater than 0!");
    }
}