using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Data;
using MinimalApiShop.Models;
using MinimalApiShop.Requests;
using MinimalApiShop.Responses;
using MinimalApiShop.Services;

namespace MinimalApiShop.Endpoints;

public static class ProductShop
{
    public static void UseProductShopEndpoints(this WebApplication app)
    {
        app.MapGet("/api/Shop/products/{id}",
    async
        (InternetShopContext _shopContext,
        [FromServices] IProductService _productService,
        [FromRoute] int id) =>
    {
        var product = await _productService.GetProductById(id);

        return Results.Ok(new ProductResponse(product));
    });

        app.MapGet("/api/Shop/products",
            async
                (InternetShopContext _shopContext,
                [FromServices] IProductService _productService,
                [FromQuery] Category category) =>
            {
                var products = await _productService.GetProductsByCategory(category);
                return Results.Ok(new ProductsResponse(products));
            });

        app.MapPost("/api/Shop/products",
            async
                (InternetShopContext _shopContext,
                [FromServices] IProductService _productService,
                [FromBody] ProductCreateRequest productRequest) =>
            {
                await _productService.AddProduct(productRequest);
                return Results.Ok(new ResultResponse("Product is added to DataBase!"));
            });

        app.MapPost("/api/Shop/product/{id}/attribute",
            async
                (InternetShopContext _shopContext,
                [FromServices] IProductService _productService,
                [FromServices] IValidator<ProductAddAtributeRequest> _validator,
                [FromBody] ProductAddAtributeRequest request,
                [FromRoute] int id) =>
            {
                await _validator.ValidateAsync(request);
                await _productService.ChangeProductAtribute(id, request.Atribute);

                return Results.Ok(new ResultResponse("Attribute is changed!"));
            });

        app.MapPost("/apiShop/products/{id}/quantity",
            async
                (InternetShopContext _shopContext,
                [FromServices] IProductService _productService,
                [FromServices] IValidator<ProductAddQuantityRequest> _validator,
                [FromRoute] int id,
                [FromBody] ProductAddQuantityRequest request) =>
            {
                await _validator.ValidateAsync(request);
                await _productService.AddQuantityProduct(id, request.Quantity);

                return Results.Ok(new ResultResponse("Quantity is changed!"));
            });

        app.MapPut("/api/Shop/product/{id}/attribute",
            async
                (InternetShopContext _shopContext,
                [FromServices] IProductService _productService,
                [FromServices] IValidator<ProductChangeAtributeRequest> _validator,
                [FromRoute] int id,
                [FromBody] ProductChangeAtributeRequest request
                ) =>
            {
                await _validator.ValidateAsync(request);
                await _productService.ChangeProductAtribute(id, request.Atribute);

                return Results.Ok(new ResultResponse("Atribute is changed!"));
            });

        app.MapDelete("/api/Shop/products/{id}",
            async
                (InternetShopContext _shopContext,
                [FromServices] IProductService _productService,
                [FromRoute] int id) =>
            {
                await _productService.DeleteProduct(id);

                return Results.Ok(new ResultResponse($"Product with id: {id} deleted!!"));
            });
    }
}
