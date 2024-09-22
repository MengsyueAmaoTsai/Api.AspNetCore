using Microsoft.Extensions.Logging;

using RichillCapital.Http;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Max;

internal sealed class MaxResponseHandler(
    ILogger<MaxResponseHandler> _logger)
{
    internal async Task<Result<TResponse>> HandleAsync<TResponse>(
        HttpResponseMessage httpResponseMessage)
    {
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            return await HandleErrorResponseAsync<TResponse>(httpResponseMessage);
        }

        return await HandleSuccessResponseAsync<TResponse>(httpResponseMessage);
    }

    internal async Task<Result> HandleAsync(
        HttpResponseMessage httpResponseMessage)
    {
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            return await HandleErrorResponseAsync(httpResponseMessage);
        }

        return Result.Success;
    }

    private async Task<Result<TResponse>> HandleErrorResponseAsync<TResponse>(
        HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default)
    {
        var error = await httpResponseMessage.ReadAsErrorAsync(cancellationToken);

        return Result<TResponse>.Failure(error);
    }

    private async Task<Result> HandleErrorResponseAsync(
        HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default)
    {
        var error = await httpResponseMessage.ReadAsErrorAsync(cancellationToken);

        return Result.Failure(error);
    }

    private async Task<Result<TResponse>> HandleSuccessResponseAsync<TResponse>(
        HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpResponseMessage.ReadAsAsync<TResponse>(cancellationToken);
            return Result<TResponse>.With(response!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to deserialize response");
            return Result<TResponse>.Failure(Error.Unexpected(ex.Message));
        }
    }
}