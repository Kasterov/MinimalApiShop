using MinimalApiShop.Models.Users;
using MinimalApiShop.Requests.Users;

namespace MinimalApiShop.Services.Users;

public interface IUserService
{
    public User Registration(CreateUserRequest request);
}
