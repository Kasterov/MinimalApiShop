using MinimalApiShop.Models.Users;
using MinimalApiShop.Requests.Users;

namespace MinimalApiShop.Services.Users;

public interface IUserService
{
    Task Registration(UserRequest request);
    Task<string> Login(UserRequest request);
}
