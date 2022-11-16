using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;
using MinimalApiShop.Models.Users;
using MinimalApiShop.Requests.Users;
using System.Security.Cryptography;

namespace MinimalApiShop.Services.Users;

public class UserService : IUserService
{
    private readonly InternetShopContext shopDbContext;
    private readonly IJwtGenerator jwtGenerator;
    private readonly IVerifyPasswordService verifyPasswordService;

    public UserService(
        InternetShopContext context,
        IJwtGenerator jwtGenerator,
        IVerifyPasswordService verifyPasswordService)
    {
        shopDbContext = context;
        this.jwtGenerator = jwtGenerator;
        this.verifyPasswordService = verifyPasswordService;
    }

    public async Task Registration(UserRequest request)
    {
        if (await IsUserExist(request.Name))
        {
            throw new InvalidDataException("User with such name already exist!");
        }

        GeneratePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User()
        {
            Name = request.Name,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = UserRole.User
        };

        await shopDbContext.Users.AddAsync(user);
        await shopDbContext.SaveChangesAsync();
    }

    public async Task<string> Login(UserRequest request)
    {
        var user = await shopDbContext.Users
            .FirstOrDefaultAsync(x => x.Name == request.Name);

        if (user is null)
        {
            throw new InvalidDataException("No user with such name!");
        }

        if (!verifyPasswordService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new InvalidDataException("Wrong password!");
        }

        return jwtGenerator.GenerateJwtToken(user);
    }

    private void GeneratePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
    {
        using (var hmacsha = new HMACSHA512())
        {
            passwordSalt = hmacsha.Key;
            passwordHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private async Task<bool> IsUserExist(string name)
    {
        if (await shopDbContext.Users.AnyAsync(x => x.Name == name))
        {
            return true;
        }

        return false;
    }
}
