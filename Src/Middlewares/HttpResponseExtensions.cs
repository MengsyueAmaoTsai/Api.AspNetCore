using RichillCapital.Serialization;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Api.Middlewares;

internal static class HttpResponseExtensions
{
    internal static async Task<Result<string>> ReadProblemDetailsAsync(
        this HttpResponse httpResponse,
        CancellationToken cancellationToken = default)
    {
        httpResponse.Body.Seek(0, SeekOrigin.Begin);

        using var reader = new StreamReader(httpResponse.Body);

        var problemDetails = await reader.ReadToEndAsync(cancellationToken);

        httpResponse.Body.Seek(0, SeekOrigin.End);

        return !problemDetails.IsValidJson() ?
            Result<string>.Failure(Error.Invalid("Problem details is not a valid JSON.")) :
            Result<string>.With(problemDetails);
    }
}