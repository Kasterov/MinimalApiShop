using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Requests.Products;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Products;

namespace MinimalApiShop.Endpoints.ProductEndpoints;

public static class ChangeAtrbuteProduct
{
    public static void ChangeAtributeProductEndpoint(this WebApplication app)
    {
        app.MapPut("/api/Shop/product/{id}/attribute", [Authorize(Roles = "Admin, UserTrader")] async
            ([FromServices] IProductService _productService,
            [FromServices] IValidator<ProductChangeAtributeRequest> _validator,
            [FromRoute] int id,
            [FromBody] ProductChangeAtributeRequest request) =>
        {
            await _validator.ValidateAsync(request);
            await _productService.ChangeProductAtribute(id, request.Atribute);

            return Results.Ok(new ResultResponse("Atribute is changed!"));
        }).WithTags("Product");
    }
}
