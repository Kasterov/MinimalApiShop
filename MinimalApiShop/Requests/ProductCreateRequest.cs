using MinimalApiShop.Models;

namespace MinimalApiShop.Requests;

public sealed record ProductCreateRequest(
    string Name,
    Category Category,
    string? Atribute,
    int Quantity
    );
