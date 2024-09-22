using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RichillCapital.Http;
using RichillCapital.Max.Authentication;
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
    private const string ApiKey = "aq3hYs749TbrH9620dygXwoxby4TlEYOoDdoBjXH";
    private const string SecretKey = "NqfZFUWlIlvegz6aW4xjDOCpLQS9ExjD7DV4PFQy";

    public async Task<Result<MaxServerTimeResponse>> GetServerTimeAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("/api/v3/timestamp", cancellationToken);
        return await HandleResponse<MaxServerTimeResponse>(response);
    }

    public async Task<Result<MaxMarketResponse[]>> ListMarketsAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("/api/v3/markets", cancellationToken);
        return await HandleResponse<MaxMarketResponse[]>(response);
    }

    public async Task<Result<MaxCurrencyResponse[]>> ListCurrenciesAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("/api/v3/currencies", cancellationToken);
        return await HandleResponse<MaxCurrencyResponse[]>(response);
    }

    public async Task<Result<MaxAccountBalanceResponse[]>> ListAccountBalancesAsync(
        string pathWalletType,
        CancellationToken cancellationToken = default)
    {
        var path = $"/api/v3/wallet/{pathWalletType}/accounts";
        var nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var parametersToSign = new
        {
            nonce,
            path,
        };

        var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(parametersToSign)));
        var signature = _signatureHandler.Sign(SecretKey, payload);

        var url = path + $"?nonce={nonce}";

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)
            .AddAuthenticationHeaders(ApiKey, payload, signature);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
        return await HandleResponse<MaxAccountBalanceResponse[]>(response);
    }

    public async Task<Result<MaxUserInfoResponse>> GetUserInfoAsync(CancellationToken cancellationToken = default)
    {
        var path = "/api/v3/info";
        var nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var parametersToSign = new
        {
            nonce,
            path,
        };

        var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(parametersToSign)));
        var signature = _signatureHandler.Sign(SecretKey, payload);

        var url = path + $"?nonce={nonce}";

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)
            .AddAuthenticationHeaders(ApiKey, payload, signature);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
        return await HandleResponse<MaxUserInfoResponse>(response);
    }

    public async Task<Result> SubmitOrderAsync(
        string pathWalletType,
        CancellationToken cancellationToken = default)
    {
        var path = $"/api/v3/wallet/{pathWalletType}/order";
        var nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var parametersToSign = new
        {
            market = "btcusdt",
            side = "buy",
            volume = 1,
            price = 100,
            nonce,
            path,
        };

        var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(parametersToSign)));
        var signature = _signatureHandler.Sign(SecretKey, payload);

        var url = path + $"?nonce={nonce}&market=btcusdt&side=buy&volume=1&price=100";

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            .AddAuthenticationHeaders(ApiKey, payload, signature);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse(response);
    }

    private async Task<Result<TMaxResponse>> HandleResponse<TMaxResponse>(HttpResponseMessage httpResponse)
    {
        if (!httpResponse.IsSuccessStatusCode)
        {
            var error = await httpResponse.ReadAsErrorAsync();
            _logger.LogWarning("{Error}", error);
            return Result<TMaxResponse>.Failure(error);
        }

        try
        {
            var response = await httpResponse.ReadAsAsync<TMaxResponse>();
            return Result<TMaxResponse>.With(response!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to deserialize response");
            return Result<TMaxResponse>.Failure(Error.Unexpected("Max.HandleResponse", ex.Message));
        }
    }

    private async Task<Result> HandleResponse(HttpResponseMessage httpResponse)
    {
        if (!httpResponse.IsSuccessStatusCode)
        {
            var error = await httpResponse.ReadAsErrorAsync();
            _logger.LogWarning("{Error}", error);
            return Result.Failure(error);
        }

        return Result.Success;
    }
}
