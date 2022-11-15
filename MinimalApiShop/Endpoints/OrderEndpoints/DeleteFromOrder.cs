using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Services.Orders;

namespace MinimalApiShop.Endpoints.OrderEndpoints;

public static class DeleteFromOrder
{
    public static void DeleteFromOrderEndpoint(this WebApplication app)
    {
        app.MapDelete("api/Shop/order/{id}", [Authorize(Roles = "User")] async
            ([FromServices] IOrderService _orderService,
            [FromRoute]int id) =>
        {
            await _orderService.DeleteFromOrder(id);
        }).WithTags("Order");
    }
}
