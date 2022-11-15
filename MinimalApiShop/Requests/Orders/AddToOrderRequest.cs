namespace MinimalApiShop.Requests.Orders;

public sealed record OrderRequest(
    int ProductId,
    int Quantity);
