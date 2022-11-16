using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Requests.Orders;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Orders;

namespace MinimalApiShop.Endpoints.OrderEndpoints;

public static class ChangeOrder
{
    public static void ChangeOrderEndpoint(this WebApplication app)
    {
        app.MapPut("/api/Shop/order/{id}/quantity", [Authorize(Roles = "User")] async
            ([FromServices] IOrderService orderService,
            [FromServices] IValidator<ChangeOrderQuantityRequest> validator,
            [FromRoute] int id,
            [FromBody] ChangeOrderQuantityRequest request) =>
        {
            await validator.ValidateAndThrowAsync(request);
            await orderService.ChangeOrder(id, request);
            return Results.Ok(new ResultResponse($"Order with product id {id} changed!"));
        }).WithTags("Order");
    }
}
