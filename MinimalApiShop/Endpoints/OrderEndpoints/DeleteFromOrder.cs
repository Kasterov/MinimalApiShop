using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Orders;

namespace MinimalApiShop.Endpoints.OrderEndpoints;

public static class DeleteFromOrder
{
    public static void DeleteFromOrderEndpoint(this WebApplication app)
    {
        app.MapDelete("api/Shop/order/{id}", [Authorize(Roles = "User")] async
            ([FromServices] IOrderService orderService,
            [FromRoute]int id) =>
        {
            await orderService.DeleteFromOrder(id);
            return Results.Ok(new ResultResponse($"You order with id {id} deleted from list of orders!"));
        }).WithTags("Order");
    }
}
