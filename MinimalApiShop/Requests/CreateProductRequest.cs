using MinimalApiShop.Models;

namespace MinimalApiShop.Requests;

public sealed record CreateProductRequest(
    string Name,
    Category Category,
    string? Atribute,
    int Quantity
    );
