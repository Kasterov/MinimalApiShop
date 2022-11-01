using MinimalApiShop.Models.Users;
using MinimalApiShop.Requests.Users;
using System.Security.Cryptography;

namespace MinimalApiShop.Services.Users;

public class UserService : IUserService
{
    public User Registration(CreateUserRequest request)
    {
        GeneratePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User()
        {
            Name = request.Name,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        return user;
    }
    private void GeneratePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
    {
        using (var hmasha = new HMACSHA512())
        {
            passwordSalt = hmasha.Key;
            passwordHash = hmasha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
