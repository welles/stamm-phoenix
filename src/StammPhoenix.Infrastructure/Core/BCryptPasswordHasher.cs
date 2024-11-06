using BCrypt.Net;
using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Infrastructure.Core;

public class BCryptPasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 16;

    private const HashType HashType = BCrypt.Net.HashType.SHA512;

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(
            password,
            BCryptPasswordHasher.HashType,
            BCryptPasswordHasher.WorkFactor
        );
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash, BCryptPasswordHasher.HashType);
    }
}
