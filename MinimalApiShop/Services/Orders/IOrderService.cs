using MinimalApiShop.Requests.Orders;

namespace MinimalApiShop.Services.Orders;

public interface IOrderService
{
    Task AddToOrder(AddToOrderRequest request);
}
