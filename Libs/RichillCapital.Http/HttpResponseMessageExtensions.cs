using Newtonsoft.Json;

namespace RichillCapital.Http;

public static class HttpResponseMessageExtensions
{
    public static async Task<TResponse> ReadAsAsync<TResponse>(
        this HttpResponseMessage httpResponse,
        CancellationToken cancellationToken = default)
    {
        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        return JsonConvert.DeserializeObject<TResponse>(content)!;
    }
}