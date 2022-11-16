using FluentValidation;

namespace MinimalApiShop.Requests.Orders.Validators;

public class ChangeOrderQuantityRequestValidator : AbstractValidator<ChangeOrderQuantityRequest>
{
    public ChangeOrderQuantityRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity have to be grater than 0!");
    }
}
