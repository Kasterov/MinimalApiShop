using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Data;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Products;

namespace MinimalApiShop.Endpoints.ProductEndpoints;

public static class GetProductById
{
    public static void GetProductByIdEndpoint(this WebApplication app)
    {
        app.MapGet("/api/Shop/products/{id}", async
            (InternetShopContext _shopContext,
            [FromServices] IProductService _productService,
            [FromRoute] int id) =>
        {
            var product = await _productService.GetProductById(id);

            return Results.Ok(new ProductResponse(product));
        }).WithTags("Shop");
    }
}
