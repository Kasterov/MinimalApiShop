using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Requests.Orders;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Orders;

namespace MinimalApiShop.Endpoints.OrderEndpoints;

public static class AddOrder
{
    public static void AddOrderEndpoint(this WebApplication app)
    {
        app.MapPost("/api/Shop/order", [Authorize(Roles = "User")] async
           ([FromServices] IOrderService _orderService,
           [FromServices] IValidator<OrderRequest> _validator,
           [FromBody] OrderRequest request) =>
        {
            await _validator.ValidateAndThrowAsync(request);
            await _orderService.AddToOrders(request);
            return Results.Ok(new ResultResponse("Product added to your order!"));
        }).WithTags("Order");
    }
}
