using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MinimalApiShop.Services.Users;

public class UserIdentity : IIdentity
{
    private readonly IHttpContextAccessor _contextAccessor;
    public UserIdentity(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public string UserId => _contextAccessor.HttpContext!.User.FindFirstValue(JwtRegisteredClaimNames.Sid);
}
