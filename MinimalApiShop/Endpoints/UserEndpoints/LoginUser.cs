using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Requests.Users;
using MinimalApiShop.Services.Users;

namespace MinimalApiShop.Endpoints.UserEndpoints;

public static class LoginUser
{
    public static void LoginUserEndpoint(this WebApplication app)
    {
        app.MapPost("/api/user/login", async
        ([FromServices] IUserService userService,
        UserRequest request) =>
        {
            return await userService.Login(request);
        }).WithTags("User");
    }
}
