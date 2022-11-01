using MinimalApiShop.Models.Products;

namespace MinimalApiShop.Requests.Products;

public sealed record ProductCreateRequest(
    string Name,
    Category Category,
    string Atribute,
    int Quantity
    );
