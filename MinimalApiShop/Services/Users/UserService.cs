using MinimalApiShop.Data;
using MinimalApiShop.Models.Users;
using MinimalApiShop.Requests.Users;
using System.Security.Cryptography;

namespace MinimalApiShop.Services.Users;

public class UserService : IUserService
{
    
    private readonly InternetShopContext _shopContext;

    public UserService(InternetShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public User Registration(UserRequest request)
    {
        GeneratePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User()
        {
            Name = request.Name,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        _shopContext.Users.Add(user);
        _shopContext.SaveChanges();
        return user;
    }

    public bool Login(UserRequest request)
    {
        return true;
    }

    private void GeneratePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
    {
        using (var hmasha = new HMACSHA512())
        {
            passwordSalt = hmasha.Key;
            passwordHash = hmasha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmasha = new HMACSHA512(passwordSalt))
        {
            var computeHash = hmasha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computeHash.SequenceEqual(passwordHash);
        }
    }
}
