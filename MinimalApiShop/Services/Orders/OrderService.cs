using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;
using MinimalApiShop.Models.Orders;
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

    public async Task AddToOrder(AddToOrderRequest request)
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
        };

        await _shopDbContext.Orders.AddAsync(order);
        await _shopDbContext.SaveChangesAsync();
    }

    private Task<bool> IsProductExist(int id) =>
        _shopDbContext.Products
            .AnyAsync(x => x.Id == id);
}
