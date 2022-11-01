using MinimalApiShop.Models.Users;
using MinimalApiShop.Requests.Users;

namespace MinimalApiShop.Services.Users;

public interface IUserService
{
    public User Registration(UserRequest request);
    public bool Login(UserRequest request);
}
