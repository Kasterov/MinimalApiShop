using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;
using MinimalApiShop.Models.Orders;
using MinimalApiShop.Models.Products;
using MinimalApiShop.Requests.Orders;
using MinimalApiShop.Services.Users;

namespace MinimalApiShop.Services.Orders;

public class OrderService : IOrderService
{
    private readonly InternetShopContext _shopDbContext;
    private readonly IIdentity _identity;
    public OrderService(InternetShopContext context, IIdentity identity)
    {
        _shopDbContext = context;
        _identity = identity;
    }

    public async Task AddToOrder(OrderRequest request)
    {
        if (!await IsProductExist(request.ProductId))
        {
            throw new ArgumentOutOfRangeException("No product with such id!");
        }

        int userId = Convert.ToInt32(_identity.UserId);

        var order = new Order
        {
            UserId = userId,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        await DecreaseQuantity(request.ProductId, request.Quantity);

        await _shopDbContext.Orders.AddAsync(order);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task DeleteFromOrder(int productId)
    {
        if (!await IsProductInOrder(productId))
        {
            throw new ArgumentOutOfRangeException("No product with such id in your order!");
        }

        var order = await GetOrderByProductId(productId);
        var productFromOrder = await GetProduct(productId);

        productFromOrder.Quantity += order.Quantity;

        _shopDbContext.Orders.Remove(order);
        await _shopDbContext.SaveChangesAsync();
    }

    public Task ChangeOrder(OrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task PurchaseOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> IsProductExist(int id) =>
        await _shopDbContext.Products
            .AnyAsync(x => x.Id == id);

    private async Task<Product> GetProduct(int id) =>
       await _shopDbContext.Products
            .SingleAsync(x => x.Id == id);

    private async Task<Order?> GetOrderByProductId(int id)
    {
        return await _shopDbContext.Orders
            .FirstOrDefaultAsync(x => x.ProductId == id && x.UserId == Convert.ToInt32(_identity.UserId));
    }

    private async Task DecreaseQuantity(int id, int quantity)
    {
        var product = await GetProduct(id);

        if (quantity > product.Quantity)
        {
            throw new InvalidDataException("Request quantity bigger than are available!");
        }

        product.Quantity -= quantity;
    }

    private async Task<bool> IsProductInOrder(int id)
    {
        return await _shopDbContext.Orders.AnyAsync(x => 
        x.ProductId == id && x.UserId == Convert.ToInt32(_identity.UserId));
    }
}
