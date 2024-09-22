using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RichillCapital.Max.Authentication;
using RichillCapital.Max.Contracts;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Max;

internal sealed class MaxRestClient(
    ILogger<MaxRestClient> _logger,
    HttpClient _httpClient,
    MaxSignatureHandler _signatureHandler,
    MaxResponseHandler _responseHandler) :
    IMaxRestClient
{
    private const string ApiKey = "aq3hYs749TbrH9620dygXwoxby4TlEYOoDdoBjXH";
    private const string SecretKey = "NqfZFUWlIlvegz6aW4xjDOCpLQS9ExjD7DV4PFQy";

    public async Task<Result<MaxServerTimeResponse>> GetServerTimeAsync(
        CancellationToken cancellationToken = default) =>
        await InvokeRequestAsync<MaxServerTimeResponse>(
            HttpMethod.Get,
            "/api/v3/timestamp",
            [],
            false,
            cancellationToken);

    public async Task<Result<MaxMarketResponse[]>> ListMarketsAsync(
        CancellationToken cancellationToken = default) =>
        await InvokeRequestAsync<MaxMarketResponse[]>(
            HttpMethod.Get,
            "/api/v3/markets",
            [],
            false,
            cancellationToken);

    public async Task<Result<MaxCurrencyResponse[]>> ListCurrenciesAsync(
        CancellationToken cancellationToken = default) =>
        await InvokeRequestAsync<MaxCurrencyResponse[]>(
            HttpMethod.Get,
            "/api/v3/currencies",
            [],
            false,
            cancellationToken);

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
            .AttachAuthenticationHeaderValues(ApiKey, payload, signature);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await _responseHandler.HandleAsync(response);
    }

    public async Task<Result<MaxAccountBalanceResponse[]>> ListAccountBalancesAsync(
        string walletType,
        CancellationToken cancellationToken = default) =>
        await InvokeRequestAsync<MaxAccountBalanceResponse[]>(
            HttpMethod.Get,
            $"/api/v3/wallet/{walletType}/accounts",
            [],
            true,
            cancellationToken);

    public async Task<Result<MaxUserInfoResponse>> GetUserInfoAsync(CancellationToken cancellationToken = default) =>
        await InvokeRequestAsync<MaxUserInfoResponse>(
            HttpMethod.Get,
            "/api/v3/info",
            [],
            true,
            cancellationToken);

    public async Task<Result<MaxOrderResponse[]>> ListOpenOrdersAsync(
        string walletType,
        CancellationToken cancellationToken = default) =>
        await InvokeRequestAsync<MaxOrderResponse[]>(
            HttpMethod.Get,
            $"/api/v3/wallet/{walletType}/orders/open",
            [],
            true,
            cancellationToken);

    public async Task<Result<MaxOrderResponse[]>> ListClosedOrdersAsync(
        string walletType,
        CancellationToken cancellationToken = default) =>
        await InvokeRequestAsync<MaxOrderResponse[]>(
            HttpMethod.Get,
            $"/api/v3/wallet/{walletType}/orders/closed",
            [],
            true,
            cancellationToken);

    public async Task<Result<MaxCancelAllOrdersResponse[]>> CancelAllOrdersAsync(
        string walletType,
        CancellationToken cancellationToken = default) =>
        await InvokeRequestAsync<MaxCancelAllOrdersResponse[]>(
            HttpMethod.Delete,
            $"/api/v3/wallet/{walletType}/orders",
            new Dictionary<string, object>(),
            true,
            cancellationToken);

    public async Task<Result<MaxCancelOrderResponse>> CancelOrderAsync(
        CancellationToken cancellationToken = default) =>
        await InvokeRequestAsync<MaxCancelOrderResponse>(
            HttpMethod.Delete,
            "/api/v3/order",
            new Dictionary<string, object>(),
            true,
            cancellationToken);

    public async Task<Result<MaxOrderResponse>> GetOrderAsync(
        string orderId,
        string clientOrderId,
        CancellationToken cancellationToken = default) =>
        await InvokeRequestAsync<MaxOrderResponse>(
            HttpMethod.Get,
            "/api/v3/order",
            new Dictionary<string, object>
            {
                { "id", orderId },
                { "client_oid", clientOrderId },
            },
            true,
            cancellationToken);

    public async Task<Result<MaxOrderResponse[]>> ListOrderHistoryAsync(
        string walletType,
        string market,
        CancellationToken cancellationToken = default)
    {
        return await InvokeRequestAsync<MaxOrderResponse[]>(
            HttpMethod.Get,
            $"/api/v3/wallet/{walletType}/orders/history",
            new Dictionary<string, object>
            {
                { "market", market },
            },
            true,
            cancellationToken);
    }

    private async Task<Result<TResponse>> InvokeRequestAsync<TResponse>(
        HttpMethod method,
        string path,
        Dictionary<string, object> parameters,
        bool requiresAuthentication,
        CancellationToken cancellationToken = default)
    {
        if (requiresAuthentication)
        {
            parameters["nonce"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        var request = CreateRequest(method, path, parameters, requiresAuthentication);

        var response = await _httpClient.SendAsync(request, cancellationToken);

        return await _responseHandler.HandleAsync<TResponse>(response, cancellationToken);
    }

    private static string BuildRequestPath(string path, Dictionary<string, object> parameters)
    {
        var requestPath = path;

        if (parameters.Count > 0)
        {
            var queryString = string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"));
            requestPath = $"{path}?{queryString}";
        }

        return requestPath;
    }

    private HttpRequestMessage CreateRequest(
        HttpMethod method,
        string path,
        Dictionary<string, object> parameters,
        bool requiresAuthentication)
    {
        var request = new HttpRequestMessage(method, BuildRequestPath(path, parameters));

        if (requiresAuthentication)
        {
            Dictionary<string, object> parametersToSign = [];

            parametersToSign.Add("path", path);

            foreach (var parameter in parameters)
            {
                parametersToSign.Add(parameter.Key, parameter.Value);
            }

            var (encodedPayload, signature) = _signatureHandler.GenerateSignature(SecretKey, parametersToSign);

            request.AttachAuthenticationHeaderValues(ApiKey, encodedPayload, signature);
        }

        return request;
    }
}
