using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;
using MinimalApiShop.Models.Orders;
using MinimalApiShop.Models.Products;
using MinimalApiShop.Requests.Orders;
using MinimalApiShop.Services.Products;
using MinimalApiShop.Services.Users;

namespace MinimalApiShop.Services.Orders;

public class OrderService : IOrderService
{
    private readonly InternetShopContext shopDbContext;
    private readonly IIdentity identityService;
    private readonly IProductService productService;
    public OrderService(
        InternetShopContext context,
        IIdentity identityService,
        IProductService productService)
    {

        shopDbContext = context;
        this.identityService = identityService;
        this.productService = productService;
    }

    public async Task AddToOrders(OrderRequest request)
    {
        if (!await productService.IsProductExist(request.ProductId))
        {
            throw new ArgumentOutOfRangeException("No product with such id!");
        }

        if (await IsProductInOrder(request.ProductId))
        {
            throw new InvalidDataException("Such order already created! You can delete it or change!");
        }

        int userId = Convert.ToInt32(identityService.UserId);

        var order = new Order
        {
            UserId = userId,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        await shopDbContext.Orders.AddAsync(order);
        await shopDbContext.SaveChangesAsync();
    }

    public async Task DeleteFromOrder(int productId)
    {
        if (!await IsProductInOrder(productId))
        {
            throw new ArgumentOutOfRangeException("No product with such id in your order!");
        }

        var order = await GetOrderByProductId(productId);

        shopDbContext.Orders.Remove(order);
        await shopDbContext.SaveChangesAsync();
    }

    public Task ChangeOrder(OrderRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        return await shopDbContext.Orders.Where(x => 
        x.UserId == Convert.ToInt32(identityService.UserId))
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
            var productInOrder = await productService
                .GetProductById(order.ProductId);

            if (order.Quantity > productInOrder.Quantity)
            {
                throw new InvalidOperationException(
                    $"Quantity of product with id {productInOrder.Id} is lesser than you required!");
            }

            productInOrder.Quantity -= order.Quantity;
        }

        shopDbContext.Orders.RemoveRange(orders);
        await shopDbContext.SaveChangesAsync();
    }

    private async Task<Order> GetOrderByProductId(int productId)
    {
        return await shopDbContext.Orders
            .SingleAsync(x => x.ProductId == productId &&
            x.UserId == Convert.ToInt32(identityService.UserId));
    }

    private async Task<bool> IsProductInOrder(int productId)
    {
        return await shopDbContext.Orders.AnyAsync(x => 
        x.ProductId == productId && x.UserId == Convert.ToInt32(identityService.UserId));
    }
}
