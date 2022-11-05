﻿using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Data;
using MinimalApiShop.Requests.Products;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Products;

namespace MinimalApiShop.Endpoints.ProductEndpoints;

public static class AddProductAtribute
{
    public static void AddProductAtributeEndpoint(this WebApplication app)
    {
        app.MapPost("/api/Shop/product/{id}/attribute", [Authorize(Roles = "Admin, User")] async
            (InternetShopContext _shopContext,
            [FromServices] IProductService _productService,
            [FromServices] IValidator<ProductAddAtributeRequest> _validator,
            [FromBody] ProductAddAtributeRequest request,
            [FromRoute] int id) =>
            {
                await _validator.ValidateAsync(request);
                await _productService.AddProductAtribute(id, request.Atribute);

                return Results.Ok(new ResultResponse("Attribute is added!"));
            }).WithTags("Shop");
    }
}