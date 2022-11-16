using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Orders;

namespace MinimalApiShop.Endpoints.OrderEndpoints;

public static class PurchaseOrder
{
    public static void PurchaseOrderEndpoint(this WebApplication app)
    {
        app.MapPost("/api/Shop/orders", [Authorize(Roles = "User")] async (
            [FromServices] IOrderService orderService) =>
        {
            await orderService.PurchaseOrders();
            return Results.Ok(new ResultResponse("You porchase all your orders!"));
        }).WithTags("Order");
    }
}
