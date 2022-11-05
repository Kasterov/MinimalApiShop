namespace MinimalApiShop.Services.Users;

public interface IVerifyPasswordService
{
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}
