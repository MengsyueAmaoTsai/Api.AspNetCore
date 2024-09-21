using System.Security.Cryptography;
using System.Text;

namespace RichillCapital.Binance.Spot;

internal sealed class SecurityProvider
{
    internal string GenerateSignature(string secretKey, string queryString)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));

        return BitConverter
            .ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(queryString)))
            .Replace("-", string.Empty)
            .ToLower();
    }
}