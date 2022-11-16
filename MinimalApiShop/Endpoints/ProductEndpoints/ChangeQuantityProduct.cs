using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Requests.Products;
using MinimalApiShop.Services.Products;
using MinimalApiShop.Responses;

namespace MinimalApiShop.Endpoints.ProductEndpoints;

public static class ChangeQuantityProduct
{
    public static void ChangeQuantityProductEndpont(this WebApplication app)
    {
        app.MapPost("/apiShop/products/{id}/quantity", [Authorize(Roles = "Admin, UserTrader")] async
           ([FromServices] IProductService _productService,
           [FromServices] IValidator<ProductAddQuantityRequest> _validator,
           [FromRoute] int id,
           [FromBody] ProductAddQuantityRequest request) =>
        {
            await _validator.ValidateAsync(request);
            await _productService.AddQuantityProduct(id, request.Quantity);

            return Results.Ok(new ResultResponse("Quantity is changed!"));
        }).WithTags("Product");
    }
}
