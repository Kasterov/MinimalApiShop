using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;
using MinimalApiShop.Models;
using MinimalApiShop.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InternetShopContext>(opt => opt
.UseSqlServer(builder.Configuration
.GetConnectionString("DefualtConnection")));

var app = builder.Build();

app.UseSwagger();

app.MapGet("/api/Shop/products/{id}", () => "There will be realization!");

app.MapGet("/api/Shop/products", () => "There will be realization!");

app.MapPost("/api/Shop/products",
    async
    ([FromServices] InternetShopContext shopContext,
    [FromBody] CreateProductRequest productRequest) =>
    {
       
    });

app.MapPost("/api/Shop/product/{id}/attribute", () => "There will be realization!");

app.MapPost("/apiShop/products/{id}quantity", () => "There will be realization!");

app.MapPut("/api/Shop/product/{id}/attribute", () => "There will be realization!");

app.MapDelete("/api/Shop/products/{id}", () => "There will be realization!");

app.UseSwaggerUI();

app.Run();