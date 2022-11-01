namespace MinimalApiShop.Responses;

public sealed record ProductResponse(
    string Name,
    int Category,
    string? Atribute,
    int Quantity);
