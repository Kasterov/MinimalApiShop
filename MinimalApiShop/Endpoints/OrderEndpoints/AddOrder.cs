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
           ([FromServices] IOrderService orderService,
           [FromServices] IValidator<OrderRequest> validator,
           [FromBody] OrderRequest request) =>
        {
            await validator.ValidateAndThrowAsync(request);
            await orderService.AddToOrders(request);
            return Results.Ok(new ResultResponse("Product added to your order!"));
        }).WithTags("Order");
    }
}
