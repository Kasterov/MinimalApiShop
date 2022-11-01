namespace MinimalApiShop.Requests.Users;

public sealed record CreateUserRequest(
    string Name,
    string Password);