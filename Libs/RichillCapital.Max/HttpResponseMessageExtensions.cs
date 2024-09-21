using System.Net;

using Newtonsoft.Json;

using RichillCapital.Max.Contracts;
using RichillCapital.SharedKernel;

namespace RichillCapital.Max;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<TResponse> ReadAsAsync<TResponse>(
        this HttpResponseMessage httpResponse,
        CancellationToken cancellationToken = default)
    {
        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        return JsonConvert.DeserializeObject<TResponse>(content)!;
    }

    internal static async Task<Error> ReadAsErrorAsync(
        this HttpResponseMessage httpResponse,
        CancellationToken cancellationToken = default)
    {
        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        return MaxErrors.Create(
            httpResponse.GetErrorType(),
            JsonConvert.DeserializeObject<MaxErrorResponse>(content)!);
    }

    private static ErrorType GetErrorType(this HttpResponseMessage response) =>
        response switch
        {
            { StatusCode: HttpStatusCode.BadRequest } => ErrorType.Validation,
            { StatusCode: HttpStatusCode.Unauthorized } => ErrorType.Unauthorized,
            { StatusCode: HttpStatusCode.Forbidden } => ErrorType.Forbidden,
            { StatusCode: HttpStatusCode.NotFound } => ErrorType.NotFound,
            { StatusCode: HttpStatusCode.Conflict } => ErrorType.Conflict,
            { StatusCode: HttpStatusCode.InternalServerError } => ErrorType.Unexpected,
            _ => ErrorType.Unexpected,
        };
}