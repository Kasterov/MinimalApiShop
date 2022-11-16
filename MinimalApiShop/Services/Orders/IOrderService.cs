using MinimalApiShop.Models.Orders;
using MinimalApiShop.Requests.Orders;

namespace MinimalApiShop.Services.Orders;

public interface IOrderService
{
    Task AddToOrders(OrderRequest request);
    Task DeleteFromOrder(int orderId);
    Task ChangeOrder(int productId, ChangeOrderQuantityRequest  request);
    Task PurchaseOrders();
    Task<IEnumerable<Order>> GetAllOrders();
    Task<Order> GetOrderByProductId(int productId); 
    Task<bool> IsProductInOrder(int productId);
}
