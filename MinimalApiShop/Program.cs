using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Configuration;
using MinimalApiShop.Data;
using MinimalApiShop.Models;
using MinimalApiShop.Requests;
using MinimalApiShop.Responses;
using MinimalApiShop.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureService();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InternetShopContext>(opt => opt
.UseSqlServer(builder.Configuration
.GetConnectionString("DefualtConnection")),ServiceLifetime.Singleton);

var app = builder.Build();

app.UseSwagger();

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
        [FromBody] ProductAddAtributeRequest request,
        [FromRoute] int id) => 
    {
        await _productService.ChangeProductAtribute(id, request.Atribute);
        return Results.Ok(new ResultResponse("Attribute is changed!"));
    });

app.MapPost("/apiShop/products/{id}/quantity", 
    async 
        (InternetShopContext _shopContext,
        [FromServices] IProductService _productService,
        [FromRoute] int id,
        [FromBody] ProductAddQuantityRequest request) =>
    {
        await _productService.AddQuantityProduct(id, request.Quantity);
        return Results.Ok(new ResultResponse("Quantity is changed!"));
    });

app.MapPut("/api/Shop/product/{id}/attribute", 
    async
        (InternetShopContext _shopContext,
        [FromServices] IProductService _productService,
        [FromRoute] int id,
        [FromBody] ProductChangeAtribute request
        ) =>
    {
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

app.UseSwaggerUI();

app.Run();