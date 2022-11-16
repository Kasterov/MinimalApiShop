using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Products;

namespace MinimalApiShop.Endpoints.ProductEndpoints;

public static class DeleteProduct
{
    public static void DeleteProductEndpoint(this WebApplication app)
    {
        app.MapDelete("/api/Shop/products/{id}", [Authorize(Roles = "Admin")] async
            ([FromServices] IProductService _productService,
            [FromRoute] int id) =>
        {
            await _productService.DeleteProduct(id);

            return Results.Ok(new ResultResponse($"Product with id: {id} deleted!!"));
        }).WithTags("Product");
    }
}
