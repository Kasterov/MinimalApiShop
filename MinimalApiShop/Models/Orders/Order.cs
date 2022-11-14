using MinimalApiShop.Models.Products;
using MinimalApiShop.Models.Users;

namespace MinimalApiShop.Models.Orders;

public class Order
{
    public User User { get; set; }
    public Product Product { get; set; }
}
