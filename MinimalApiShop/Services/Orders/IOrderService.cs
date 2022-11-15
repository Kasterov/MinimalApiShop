using MinimalApiShop.Requests.Orders;

namespace MinimalApiShop.Services.Orders;

public interface IOrderService
{
    Task AddToOrder(OrderRequest request);
    Task DeleteFromOrder(int orderId);
    Task ChangeOrder(OrderRequest request);
    Task PurchaseOrder(int orderId);
}
