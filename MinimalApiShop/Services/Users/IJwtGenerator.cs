using MinimalApiShop.Models.Users;

namespace MinimalApiShop.Services.Users;

public interface IJwtGenerator
{
    string GenerateJwtToken(User user);
}
