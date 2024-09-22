namespace RichillCapital.Max.Authentication;

internal static class HttpRequestMessageExtensions
{
    public static HttpRequestMessage AttachAuthenticationHeaderValues(
        this HttpRequestMessage request,
        string apiKey,
        string payload,
        string signature)
    {
        request.Headers.Add("X-MAX-ACCESSKEY", apiKey);
        request.Headers.Add("X-MAX-PAYLOAD", payload);
        request.Headers.Add("X-MAX-SIGNATURE", signature);

        return request;
    }
}