using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Requests.Users;
using MinimalApiShop.Services.Users;

namespace MinimalApiShop.Endpoints;

public static class User
{
    public static void UseUserEndpoints(this WebApplication app)
    {
        app.MapPost("/api/user",  
                ([FromServices] IUserService _userService,
                UserRequest request) =>
        {
            return _userService.Registration(request);
        }).WithTags("User");

        app.MapPost("/api/user/login",
                ([FromServices] IUserService _userService,
                UserRequest request) =>
                {
                    return _userService.Login(request);
                }).WithTags("User");
    }
}
