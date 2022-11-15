using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Orders;

namespace MinimalApiShop.Endpoints.OrderEndpoints;

public static class GetOrders
{
    public static void GetOrdersEndpoint(this WebApplication app)
    {
        app.MapGet("/api/Shop/orders",[Authorize(Roles = "User")] async
            ([FromServices] IOrderService orderService) =>
        {
            return Results
            .Ok(new OrdersResponse(await orderService
            .GetAllOrders()));

        }).WithTags("Order");
    }
}
