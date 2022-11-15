using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;
using MinimalApiShop.Models.Orders;
using MinimalApiShop.Requests.Orders;
using MinimalApiShop.Requests.Products;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Orders;
using MinimalApiShop.Services.Products;
using MinimalApiShop.Services.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MinimalApiShop.Endpoints.OrderEndpoints;

public static class AddOrder
{
    public static void AddOrderEndpoint(this WebApplication app)
    {
        app.MapPost("/api/Shop/order", [Authorize(Roles = "User")] async
           ([FromServices] IOrderService _orderService,
           [FromServices] IValidator<AddToOrderRequest> _validator,
           [FromBody] AddToOrderRequest request) =>
        {
            await _validator.ValidateAndThrowAsync(request);
            await _orderService.AddToOrder(request);
            return Results.Ok(new ResultResponse("Product added to your order!"));
        }).WithTags("Order");
    }
}
