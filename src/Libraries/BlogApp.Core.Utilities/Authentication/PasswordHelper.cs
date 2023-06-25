using System.Security.Cryptography;
using System.Text;

namespace BlogApp.Core.Utilities.Authentication;
public class PasswordHelper
{
    private const int KeySize = 64;
    private const int Iterations = 350000;

    public static (byte[] Key, string) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithmName.SHA512,
            KeySize);
        return (salt, Convert.ToHexString(hash));
    }

    public static bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA512, KeySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}
