using System.Security.Cryptography;
using System.Text;

namespace AuthenticationService
{
    public static class PasswordHasher
    {
        const int keySize = 64;
        const int iterations = 350000;
        static byte[] salt = RandomNumberGenerator.GetBytes(keySize);
        public static string HashPassword(string password)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                HashAlgorithmName.SHA512,
                keySize);
             return Convert.ToHexString(hash);
        }
    }
}
