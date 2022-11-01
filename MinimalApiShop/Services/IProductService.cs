using MinimalApiShop.Models;
using MinimalApiShop.Requests;

namespace MinimalApiShop.Services;

public interface IProductService
{
    Task AddProduct(CreateProductRequest productCreateRequest);
    Task DeleteProduct(int id);
    Task AddQuantityProduct(int id, int quantity);
    Task ChangeProductAtribute(int id, string atribute);
    Task AddProductAtribute(int id, string atribute);
    Task<List<Product>> GetProductsByCategory(Category category);
    Task<Product> GetProductById(int id);
}
