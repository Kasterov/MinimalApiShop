using System.Security.Cryptography;

namespace MinimalApiShop.Services.Users;

public class VerifyPasswordService : IVerifyPasswordService
{
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmacsha = new HMACSHA512(passwordSalt))
        {
            var computeHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computeHash.SequenceEqual(passwordHash);
        }
    }
}
