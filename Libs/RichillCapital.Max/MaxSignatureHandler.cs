using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace RichillCapital.Max;

internal sealed class MaxSignatureHandler(
    ILogger<MaxSignatureHandler> _logger)
{
    internal string Sign(string secretKey, string path, string payload)
    {
        _logger.LogInformation(
            "Signing with secret key: {maskedSecretKey} and payload: {payload}",
            MaskSecretKey(secretKey),
            payload);

        var nonce = DateTimeOffset.UtcNow.Millisecond;

        var bodyToEncode = new
        {
            Nonce = nonce,
            Path = path,
        };

        var encodedBody = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bodyToEncode)));

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(encodedBody));

        return BitConverter.ToString(hash).Replace("-", "").ToLower();
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