using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace RichillCapital.Max;

internal sealed class MaxSignatureHandler(
    ILogger<MaxSignatureHandler> _logger)
{
    internal (string, string) GenerateSignature(
        string secretKey,
        Dictionary<string, object> parameters)
    {
        _logger.LogInformation(
            "Signing with secret key: {maskedSecretKey} and parameters: {payload}",
            MaskSecretKey(secretKey),
            parameters.ToString());

        var encodedPayload = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(parameters)));
        var signature = Sign(secretKey, encodedPayload);

        return (encodedPayload, signature);
    }

    internal string Sign(
        string secretKey,
        string payload)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));

        return BitConverter
            .ToString(hash)
            .Replace("-", "")
            .ToLower();
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