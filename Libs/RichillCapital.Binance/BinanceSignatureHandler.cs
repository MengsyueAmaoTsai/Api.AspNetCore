using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Logging;

namespace RichillCapital.Binance;

internal sealed class BinanceSignatureHandler(
    ILogger<BinanceSignatureHandler> _logger)
{
    internal string Sign(string secretKey, string queryString)
    {
        _logger.LogInformation(
            "Signing with secret key: {maskedSecretKey} and query string: {queryString}",
            MaskSecretKey(secretKey),
            queryString);

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));

        var signature = BitConverter
            .ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(queryString)))
            .Replace("-", string.Empty)
            .ToLower();

        _logger.LogInformation("Signature: {signature}", signature);

        return signature;
    }

    private static string MaskSecretKey(string secretKey)
    {
        if (string.IsNullOrEmpty(secretKey) || secretKey.Length <= 6)
        {
            return new string('*', secretKey.Length);
        }

        var start = secretKey.Substring(0, 3);
        var end = secretKey.Substring(secretKey.Length - 3);

        return $"{start}{new string('*', secretKey.Length - 6)}{end}";
    }
}