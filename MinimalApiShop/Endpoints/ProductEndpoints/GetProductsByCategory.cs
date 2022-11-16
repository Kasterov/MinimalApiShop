using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Models.Products;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Products;

namespace MinimalApiShop.Endpoints.ProductEndpoints;

public static class GetProductsByCategory
{
    public static void GetProductsByCategoryEndpoint(this WebApplication app)
    {
        app.MapGet("/api/Shop/products", [AllowAnonymous] async
           ([FromServices] IProductService _productService,
           [FromQuery] Category category) =>
        {
            var products = await _productService.GetProductsByCategory(category);

            return Results.Ok(new ProductsResponse(products));
        }).WithTags("Product");
    }
}
