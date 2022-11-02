using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Requests.Users;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Users;

namespace MinimalApiShop.Endpoints;

public static class User
{
    public static void UseUserEndpoints(this WebApplication app)
    {
        app.MapPost("/api/user", async
        ([FromServices] IUserService _userService,
        UserRequest request) =>
        {
            await _userService.Registration(request);
            return Results.Ok(new UserResponse("User registered!"));
        }).WithTags("User");

        app.MapPost("/api/user/login", async
        ([FromServices] IUserService _userService,
        UserRequest request) =>
        {
            return await _userService.Login(request);
        }).WithTags("User");
    }
}
