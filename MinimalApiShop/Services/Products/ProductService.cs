using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;
using MinimalApiShop.Models.Products;
using MinimalApiShop.Requests.Products;

namespace MinimalApiShop.Services.Products;

public class ProductService : IProductService
{
    private readonly InternetShopContext _shopDbContext;
    public ProductService(InternetShopContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }

    public async Task AddProduct(ProductCreateRequest productRequest)
    {
        var product = new Product()
        {
            Name = productRequest.Name,
            Category = productRequest.Category,
            Quantity = productRequest.Quantity,
            Atribute = productRequest.Atribute
        };

        await _shopDbContext.Products.AddAsync(product);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task AddProductAtribute(int id, string atribute)
    {
        if (!await IsProductExist(id))
        {
            throw new ArgumentOutOfRangeException("No product with such id!");
        }

        var product = await GetProduct(id);

        if (product.Atribute != "")
        {
            throw new ArgumentException("This product already have atribute!");
        }

        product.Atribute = atribute;
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task AddQuantityProduct(int id, int quantity)
    {
        if (!await IsProductExist(id))
        {
            throw new ArgumentOutOfRangeException("No product with such id!");
        }

        var product = await GetProduct(id);

        product.Quantity = quantity;
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task ChangeProductAtribute(int id, string atribute)
    {
        if (!await IsProductExist(id))
        {
            throw new ArgumentOutOfRangeException("No product with such id!");
        }

        var product = await GetProduct(id);

        if (product.Atribute == "")
        {
            throw new ArgumentException("This product have NO atribute! Add atribute firstly!");
        }

        product.Atribute = atribute;

        await _shopDbContext.SaveChangesAsync();
    }

    public async Task DeleteProduct(int id)
    {
        if (!await IsProductExist(id))
        {
            throw new ArgumentOutOfRangeException();
        }

        var product = await GetProduct(id);

        _shopDbContext.Products.Remove(product);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task<Product> GetProductById(int id)
    {
        if (!await IsProductExist(id))
        {
            throw new ArgumentOutOfRangeException("No product with such id!");
        }

        return _shopDbContext.Products.Single(x => x.Id == id);
    }

    public async Task<List<Product>> GetProductsByCategory(Category category)
    {
        var products =
            await _shopDbContext.Products
            .Where(x => x.Category == category)
            .Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                Atribute = x.Atribute
            })
            .ToListAsync();

        if (products.Count == 0)
        {
            throw new ArgumentOutOfRangeException("No product with such category!");
        }

        return products;
    }

    private Task<bool> IsProductExist(int id) =>
        _shopDbContext.Products
            .AnyAsync(x => x.Id == id);

    private Task<Product> GetProduct(int id) =>
        _shopDbContext.Products
            .SingleAsync(x => x.Id == id);
}
