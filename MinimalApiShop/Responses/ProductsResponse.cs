using MinimalApiShop.Models.Products;

namespace MinimalApiShop.Responses;

public sealed record ProductsResponse(
    IEnumerable<Product> List);
