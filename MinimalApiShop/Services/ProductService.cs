using MinimalApiShop.Models;
using MinimalApiShop.Requests;

namespace MinimalApiShop.Services;

public class ProductService : IProductService
{
    public Task AddProduct(CreateProductRequest productCreateRequest)
    {
        throw new NotImplementedException();
    }

    public Task AddProductAtribute(int id, string atribute)
    {
        throw new NotImplementedException();
    }

    public Task AddQuantityProduct(int id, int quantity)
    {
        throw new NotImplementedException();
    }

    public Task ChangeProductAtribute(int id, string atribute)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsByCategory(Category category)
    {
        throw new NotImplementedException();
    }
}
