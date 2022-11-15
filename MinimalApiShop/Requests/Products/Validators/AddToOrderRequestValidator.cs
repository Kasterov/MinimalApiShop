using FluentValidation;
using MinimalApiShop.Requests.Orders;

namespace MinimalApiShop.Requests.Products.Validators;

public class AddToOrderRequestValidator : AbstractValidator<OrderRequest>
{
    public AddToOrderRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity have to be grater than 0!");
    }
}
