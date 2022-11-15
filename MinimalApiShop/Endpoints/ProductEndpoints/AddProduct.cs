using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Requests.Products;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Products;

namespace MinimalApiShop.Endpoints.ProductEndpoints;

public static class AddProduct
{
    public static void AddProductEndpoint(this WebApplication app)
    {
        app.MapPost("/api/Shop/products", [Authorize(Roles = "Admin, User")] async
           ([FromServices] IProductService _productService,
           [FromServices] IValidator<ProductCreateRequest> _validator,
           [FromBody] ProductCreateRequest request) =>
        {
            await _validator.ValidateAndThrowAsync(request);
            await _productService.AddProduct(request);
            return Results.Ok(new ResultResponse("Product is added to DataBase!"));
        }).WithTags("Shop");
    }
}
