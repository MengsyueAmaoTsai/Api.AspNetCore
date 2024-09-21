using Newtonsoft.Json;

using RichillCapital.Http;
using RichillCapital.Max.Contracts;
using RichillCapital.SharedKernel;

namespace RichillCapital.Max;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<Error> ReadAsErrorAsync(
        this HttpResponseMessage httpResponse,
        CancellationToken cancellationToken = default)
    {
        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        return MaxErrors.Create(
            httpResponse.GetErrorType(),
            JsonConvert.DeserializeObject<MaxErrorResponse>(content)!);
    }
}