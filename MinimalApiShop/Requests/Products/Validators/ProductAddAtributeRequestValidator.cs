using FluentValidation;
using MinimalApiShop.Requests.Products;

namespace MinimalApiShop.Requests.Validators;

public class ProductAddAtributeRequestValidator : AbstractValidator<ProductAddAtributeRequest>
{
    public ProductAddAtributeRequestValidator()
    {
        RuleFor(x => x.Atribute)
            .MinimumLength(10)
            .MaximumLength(100).WithErrorCode("Sex")
            .WithMessage("Atribute minimun lenght is 10 and maximum 100!");
    }
}
