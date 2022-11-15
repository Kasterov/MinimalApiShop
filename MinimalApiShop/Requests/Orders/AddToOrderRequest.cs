namespace MinimalApiShop.Requests.Orders;

public sealed record AddToOrderRequest(
    int ProductId,
    int Quantity);
