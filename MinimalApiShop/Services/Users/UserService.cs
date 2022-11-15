using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;
using MinimalApiShop.Models.Users;
using MinimalApiShop.Requests.Users;
using System;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MinimalApiShop.Services.Users;

public class UserService : IUserService
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly InternetShopContext _shopContext;
    private readonly IVerifyPasswordService _verifyPasswordService;

    public UserService(
        InternetShopContext shopContext,
        IJwtGenerator jwtGenerator,
        IVerifyPasswordService verifyPasswordService)
    {
        _shopContext = shopContext;
        _jwtGenerator = jwtGenerator;
        _verifyPasswordService = verifyPasswordService;
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

        await _shopContext.Users.AddAsync(user);
        await _shopContext.SaveChangesAsync();
    }

    public async Task<string> Login(UserRequest request)
    {
        var user = await _shopContext.Users
            .FirstOrDefaultAsync(x => x.Name == request.Name);

        if (user is null)
        {
            throw new InvalidDataException("No user with such name!");
        }

        if (!_verifyPasswordService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new InvalidDataException("Wrong password!");
        }

        return _jwtGenerator.GenerateJwtToken(user);
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
        if (await _shopContext.Users.AnyAsync(x => x.Name == name))
        {
            return true;
        }

        return false;
    }
}
