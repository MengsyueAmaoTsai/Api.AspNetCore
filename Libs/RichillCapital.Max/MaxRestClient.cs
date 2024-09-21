using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RichillCapital.Max.Contracts;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Max;

internal sealed class MaxRestClient(
    ILogger<MaxRestClient> _logger,
    HttpClient _httpClient,
    MaxSignatureHandler _signatureHandler) :
    IMaxRestClient
{
    public async Task<Result<MaxServerTimeResponse>> GetServerTimeAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("api/v3/timestamp", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.ReadAsErrorAsync(cancellationToken);

            _logger.LogWarning("{Error}", error);
            return Result<MaxServerTimeResponse>.Failure(error);
        }

        try
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var serverTimeResponse = JsonConvert.DeserializeObject<MaxServerTimeResponse>(content);
            return Result<MaxServerTimeResponse>.With(serverTimeResponse!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to deserialize server time response");
            return Result<MaxServerTimeResponse>.Failure(Error.Unexpected("Max.GetServerTimeAsync", ex.Message));
        }
    }

    public async Task<Result> SubmitOrderAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v3/wallet/{pathWalletType}/order", new
        {
        });

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.ReadAsErrorAsync(cancellationToken);

            _logger.LogWarning("{Error}", error);
            return Result.Failure(Error.Unexpected("Max.SubmitOrderAsync", $"{error}"));
        }

        return Result.Success;
    }
}
