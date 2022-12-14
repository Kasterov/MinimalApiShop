using MinimalApiShop.Models.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalApiShop.Services.Users;

public class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration _builder;

    public JwtGenerator(IConfiguration builder)
    {
        _builder = builder;
    }
    public string GenerateJwtToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Name, user.Name),
            new (ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _builder.GetSection("Jwt:Issuer").Value,
            audience: _builder.GetSection("Jwt:Audience").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(1),
            notBefore: DateTime.Now,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_builder.GetSection("Jwt:Key").Value)),
                SecurityAlgorithms.HmacSha256)
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;
    }
}
