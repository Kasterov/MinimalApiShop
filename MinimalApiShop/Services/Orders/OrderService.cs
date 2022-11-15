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

    public async Task AddToOrders(OrderRequest request)
    {
        if (!await IsProductExist(request.ProductId))
        {
            throw new ArgumentOutOfRangeException("No product with such id!");
        }

        if (await IsProductInOrder(request.ProductId))
        {
            throw new InvalidDataException("Such order already created! You can delete it or change!");
        }

        int userId = Convert.ToInt32(_identity.UserId);

        var order = new Order
        {
            UserId = userId,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

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

        _shopDbContext.Orders.Remove(order);
        await _shopDbContext.SaveChangesAsync();
    }

    public Task ChangeOrder(OrderRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        return await _shopDbContext.Orders.Where(x => 
        x.UserId == Convert.ToInt32(_identity.UserId))
        .ToListAsync();
    }

    public async Task PurchaseOrders()
    {
        var orders = await GetAllOrders();

        if (!orders.Any())
        {
            throw new InvalidDataException("Your order list is empty!");
        }

        foreach (var order in orders)
        {
            var productInOrder = await GetProduct(order.ProductId);

            if (order.Quantity > productInOrder.Quantity)
            {
                throw new InvalidOperationException($"Amount of {productInOrder.Name} with id  {productInOrder.Id} is lesser than you required!");
            }

            productInOrder.Quantity -= order.Quantity;
        }

        _shopDbContext.Orders.RemoveRange(orders);
        await _shopDbContext.SaveChangesAsync();
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
