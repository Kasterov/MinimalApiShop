using MinimalApiShop.Models.Orders;
using MinimalApiShop.Requests.Orders;

namespace MinimalApiShop.Services.Orders;

public interface IOrderService
{
    Task AddToOrders(OrderRequest request);
    Task DeleteFromOrder(int orderId);
    Task ChangeOrder(OrderRequest request);
    Task PurchaseOrders();
    Task<IEnumerable<Order>> GetAllOrders();
}
