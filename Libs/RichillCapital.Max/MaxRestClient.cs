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
        var response = await _httpClient.GetAsync(MaxApiRoutes.GetServerTime, cancellationToken);
        return await HandleResponse<MaxServerTimeResponse>(response);
    }

    public async Task<Result<MaxMarketResponse[]>> ListMarketsAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(MaxApiRoutes.ListMarkets, cancellationToken);
        return await HandleResponse<MaxMarketResponse[]>(response);
    }

    public async Task<Result<MaxCurrencyResponse[]>> ListCurrenciesAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(MaxApiRoutes.ListCurrencies, cancellationToken);
        return await HandleResponse<MaxCurrencyResponse[]>(response);
    }

    public async Task<Result<MaxAccountBalanceResponse[]>> ListAccountBalancesAsync(
        string walletType,
        CancellationToken cancellationToken = default)
    {
        var path = $"/api/v3/wallet/{walletType}/accounts";
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
        var path = MaxApiRoutes.GetUserInfo;
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
        string walletType,
        CancellationToken cancellationToken = default)
    {
        var path = $"/api/v3/wallet/{walletType}/order";
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

    public async Task<Result<MaxOrderResponse[]>> ListOpenOrdersAsync(string walletType, CancellationToken cancellationToken = default)
    {
        var path = $"/api/v3/wallet/{walletType}/orders/open";
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

        return await HandleResponse<MaxOrderResponse[]>(response);
    }

    public async Task<Result<MaxOrderResponse[]>> ListClosedOrdersAsync(
        string walletType,
        CancellationToken cancellationToken = default)
    {
        var path = $"/api/v3/wallet/{walletType}/orders/closed";
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

        return await HandleResponse<MaxOrderResponse[]>(response);
    }

    public async Task<Result<MaxOrderResponse[]>> ListOrderHistoryAsync(
        string walletType,
        string market,
        CancellationToken cancellationToken = default)
    {
        var path = $"/api/v3/wallet/{walletType}/orders/history";
        var nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var parametersToSign = new
        {
            market,
            nonce,
            path,
        };

        var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(parametersToSign)));

        var signature = _signatureHandler.Sign(SecretKey, payload);

        var url = path + $"?nonce={nonce}&market={market}";

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)
            .AddAuthenticationHeaders(ApiKey, payload, signature);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<MaxOrderResponse[]>(response);
    }

    public async Task<Result<MaxCancelAllOrdersResponse[]>> CancelAllOrdersAsync(
        string walletType,
        CancellationToken cancellationToken = default)
    {
        var path = $"/api/v3/wallet/{walletType}/orders";
        var nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var parametersToSign = new
        {
            nonce,
            path,
        };

        var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(parametersToSign)));

        var signature = _signatureHandler.Sign(SecretKey, payload);

        var url = path + $"?nonce={nonce}";

        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, url)
            .AddAuthenticationHeaders(ApiKey, payload, signature);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<MaxCancelAllOrdersResponse[]>(response);
    }

    public async Task<Result<MaxCancelOrderResponse>> CancelOrderAsync(CancellationToken cancellationToken = default)
    {
        var path = "/api/v3/order";
        var nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var parametersToSign = new
        {
            nonce,
            path,
        };

        var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(parametersToSign)));

        var signature = _signatureHandler.Sign(SecretKey, payload);

        var url = path + $"?nonce={nonce}";

        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, url)
            .AddAuthenticationHeaders(ApiKey, payload, signature);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<MaxCancelOrderResponse>(response);
    }

    public Task<Result> GetOrderDetailAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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
