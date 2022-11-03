using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalApiShop.Data;
using MinimalApiShop.Models.Users;
using MinimalApiShop.Requests.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MinimalApiShop.Services.Users;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly InternetShopContext _shopContext;

    public UserService(InternetShopContext shopContext, IConfiguration configuration)
    {
        _shopContext = shopContext;
        _configuration = configuration;
    }

    public async Task Registration(UserRequest request)
    {
        if (_shopContext.Users.Any(x => x.Name == request.Name))
        {
            throw new InvalidDataException("User with such name already exist!");
        }

        GeneratePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User()
        {
            Name = request.Name,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
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

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new InvalidDataException("Wrong password!");
        }

        return CreateJwtToken(user);
    }

    private void GeneratePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
    {
        using (var hmacsha = new HMACSHA512())
        {
            passwordSalt = hmacsha.Key;
            passwordHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmacsha = new HMACSHA512(passwordSalt))
        {
            var computeHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computeHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateJwtToken(User user)
    {
        List<Claim> calims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name)
        };

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8
            .GetBytes(_configuration
            .GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: calims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: creds
            );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;
    }
}
