using MinimalApiShop.Models.Products;

namespace MinimalApiShop.Responses;

public sealed record ProductsResponse(
    List<Product> List);
