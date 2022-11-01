namespace MinimalApiShop.Requests.Users;

public sealed record UserRequest(
    string Name,
    string Password);