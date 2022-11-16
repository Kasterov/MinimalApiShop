using Microsoft.AspNetCore.Mvc;
using MinimalApiShop.Requests.Users;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Users;

namespace MinimalApiShop.Endpoints.UserEndpoints;

public static class AddUser
{
    public static void AddUserEndpoint(this WebApplication app)
    {
        app.MapPost("/api/user", async
        ([FromServices] IUserService userService,
        UserRequest request) =>
        {
            await userService.Registration(request);
            return Results.Ok(new UserResponse("User registered!"));
        }).WithTags("User");
    }
}
