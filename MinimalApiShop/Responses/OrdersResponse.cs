using MinimalApiShop.Models.Orders;

namespace MinimalApiShop.Responses;

public sealed record OrdersResponse(
    IEnumerable<Order> Orders);