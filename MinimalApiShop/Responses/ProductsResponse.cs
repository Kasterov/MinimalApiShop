using MinimalApiShop.Models;

namespace MinimalApiShop.Responses;

public sealed record ProductsResponse(
    List<Product> List);
