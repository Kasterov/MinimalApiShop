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
    private readonly InternetShopContext shopDbContext;
    public ProductService(InternetShopContext context)
    {
        shopDbContext = context;
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

        await shopDbContext.Products.AddAsync(product);
        await shopDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Add atribute to existing product from DB
    /// </summary>
    public async Task AddProductAtribute(int productId, string atribute)
    {
        var product = await GetProductById(productId);

        if (product.Atribute != "")
        {
            throw new ArgumentException("This product already have atribute!");
        }

        product.Atribute = atribute;
        await shopDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Add quantity to existing product from DB
    /// </summary>
    public async Task AddQuantityProduct(int productId, int quantity)
    {
        var product = await GetProductById(productId);

        product.Quantity = quantity;
        await shopDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Change atribute of existing product from DB
    /// </summary>
    public async Task ChangeProductAtribute(int productId, string atribute)
    {
        var product = await GetProductById(productId);

        if (product.Atribute == "")
        {
            throw new ArgumentException("This product have NO atribute! Add atribute firstly!");
        }

        product.Atribute = atribute;

        await shopDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Delete existing product from DB
    /// </summary>
    public async Task DeleteProduct(int productId)
    {
        var product = await GetProductById(productId);

        shopDbContext.Products.Remove(product);
        await shopDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Return product from DB by id
    /// </summary>
    public async Task<Product> GetProductById(int productId)
    {
        if (!await IsProductExist(productId))
        {
            throw new ArgumentOutOfRangeException("No product with such id!");
        }

        return await shopDbContext.Products.SingleAsync(x => x.Id == productId);
    }

    /// <summary>
    /// Return a list of products from DB by category
    /// </summary>
    public async Task<IEnumerable<Product>> GetProductsByCategory(Category category)
    {
        var products =
            await shopDbContext.Products
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

        if (!products.Any())
        {
            throw new ArgumentOutOfRangeException("No product with such category!");
        }

        return products;
    }

    /// <summary>
    /// Return true if product exist in DB and false if is not
    /// </summary>
    public async Task<bool> IsProductExist(int productId) =>
       await shopDbContext.Products
            .AnyAsync(x => x.Id == productId);
}
