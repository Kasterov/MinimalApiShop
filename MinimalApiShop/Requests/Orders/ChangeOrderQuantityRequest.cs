namespace MinimalApiShop.Requests.Orders;

public sealed record ChangeOrderQuantityRequest(
    int Quantity);