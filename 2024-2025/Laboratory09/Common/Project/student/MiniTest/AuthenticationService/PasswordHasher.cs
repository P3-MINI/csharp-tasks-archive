using System.Security.Cryptography;
using System.Text;

namespace AuthenticationService;

public static class PasswordHasher
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private static readonly byte[] Salt = RandomNumberGenerator.GetBytes(KeySize);
    public static string HashPassword(string password)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Salt,
            Iterations,
            HashAlgorithmName.SHA512,
            KeySize);
        return Convert.ToHexString(hash);
    }
}