using MinimalApiShop.Models.Products;
using MinimalApiShop.Requests.Products;

namespace MinimalApiShop.Services.Products;

public interface IProductService
{
    Task AddProduct(ProductCreateRequest productCreateRequest);
    Task DeleteProduct(int productId);
    Task AddQuantityProduct(int prouductId, int quantity);
    Task ChangeProductAtribute(int productId, string atribute);
    Task AddProductAtribute(int productId, string atribute);
    Task<IEnumerable<Product>> GetProductsByCategory(Category category);
    Task<Product> GetProductById(int productId);
    Task<bool> IsProductExist(int productId);
}
