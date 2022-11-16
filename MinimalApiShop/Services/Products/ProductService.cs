using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;
using MinimalApiShop.Models.Products;
using MinimalApiShop.Requests.Products;

namespace MinimalApiShop.Services.Products;

public class ProductService : IProductService
{
    /// <summary>
    /// Implement context of our DB
    /// </summary>
    private readonly InternetShopContext _shopDbContext;
    public ProductService(InternetShopContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }

    /// <summary>
    /// Add new product to DB by special request record
    /// </summary>
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

    /// <summary>
    /// Add atribute to existing product
    /// </summary>
    public async Task AddProductAtribute(int productId, string atribute)
    {
        var product = await GetProductById(productId);

        if (product.Atribute != "")
        {
            throw new ArgumentException("This product already have atribute!");
        }

        product.Atribute = atribute;
        await _shopDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Add quantity to existing product
    /// </summary>
    public async Task AddQuantityProduct(int productId, int quantity)
    {
        var product = await GetProductById(productId);

        product.Quantity = quantity;
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task ChangeProductAtribute(int productId, string atribute)
    {
        var product = await GetProductById(productId);

        if (product.Atribute == "")
        {
            throw new ArgumentException("This product have NO atribute! Add atribute firstly!");
        }

        product.Atribute = atribute;

        await _shopDbContext.SaveChangesAsync();
    }

    public async Task DeleteProduct(int productId)
    {
        var product = await GetProductById(productId);

        _shopDbContext.Products.Remove(product);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task<Product> GetProductById(int productId)
    {
        if (!await IsProductExist(productId))
        {
            throw new ArgumentOutOfRangeException("No product with such id!");
        }

        return await _shopDbContext.Products.SingleAsync(x => x.Id == productId);
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
                Atribute = x.Atribute,
                Quantity = x.Quantity
            })
            .ToListAsync();

        if (products.Count == 0)
        {
            throw new ArgumentOutOfRangeException("No product with such category!");
        }

        return products;
    }

    public async Task<bool> IsProductExist(int productId) =>
       await _shopDbContext.Products
            .AnyAsync(x => x.Id == productId);
}
