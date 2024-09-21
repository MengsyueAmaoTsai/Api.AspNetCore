using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance.Spot;

internal sealed class BinanceSpotRestClient(
    ILogger<BinanceSpotRestClient> _logger,
    HttpClient _httpClient,
    SecurityProvider _securityProvider) :
    IBinanceSpotRestClient
{
    private const string ApiKey = "guVqJIzZ29JZx2BTv9VbxxOr7IehQIIRRXABm53rawtThH0XcD8EeyzUtMbIaQ92";
    private const string SecretKey = "BPwSSG45zE8ABiZ6Zm4t9gJFJMo19ExjBqOQlmLcOM5LgfyYP6V5biYrsUkZfXxm";

    public async Task<Result> TestConnectivityAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            BinanceRestApiRoutes.General.TestConnectivity,
            cancellationToken);

        return await HandleResponseAsync(response);
    }

    public async Task<Result<ExchangeInfoResponse>> GetExchangeInfoAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            BinanceRestApiRoutes.General.ExchangeInfo,
            cancellationToken);

        return await HandleResponseAsync<ExchangeInfoResponse>(response);
    }

    public async Task<Result<BinanceServerTimeResponse>> CheckServerTimeAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            BinanceRestApiRoutes.General.CheckServerTime,
            cancellationToken);

        return await HandleResponseAsync<BinanceServerTimeResponse>(response);
    }

    public async Task<Result> NewOrderAsync(
        string symbol,
        string side,
        string type,
        string timeInForce,
        decimal quantity,
        decimal price,
        long recvWindow,
        long timestamp,
        CancellationToken cancellationToken = default)
    {
        var queryString = $"symbol={symbol}&side={side}&type={type}&timeInForce={timeInForce}&quantity={quantity}&price={price}&recvWindow={recvWindow}&timestamp={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
        queryString += $"&signature={_securityProvider.GenerateSignature(SecretKey, queryString)}";

        _httpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", ApiKey);

        var response = await _httpClient.PostAsync(
            "api/v3/order",
            new StringContent(queryString, Encoding.UTF8, "application/x-www-form-urlencoded"),
            cancellationToken);

        return await HandleResponseAsync(response);
    }

    private async Task<Result> HandleResponseAsync(HttpResponseMessage httpResponse)
    {
        try
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                var error = await ReadAsErrorAsync(httpResponse);

                _logger.LogError(
                    "Request is failed: {statusCode} {error}",
                    httpResponse.StatusCode,
                    error);

                return Result.Failure(error);
            }

            _logger.LogInformation("Response: {response}", responseContent);

            return Result.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to handle response");
            return Result.Failure(Error.Unexpected("Failed to handle response"));
        }
    }

    private async Task<Result<TBinanceResponse>> HandleResponseAsync<TBinanceResponse>(
        HttpResponseMessage httpResponse)
    {
        try
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                var error = await ReadAsErrorAsync(httpResponse);

                _logger.LogError(
                    "Request is failed: {statusCode} {error}",
                    httpResponse.StatusCode,
                    error);

                return Result<TBinanceResponse>.Failure(error);
            }

            var response = JsonConvert.DeserializeObject<TBinanceResponse>(responseContent);
            return Result<TBinanceResponse>.With(response!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while handling response");
            return Result<TBinanceResponse>.Failure(Error.Unexpected("Failed to handle response"));
        }
    }

    private async Task<Error> ReadAsErrorAsync(
        HttpResponseMessage httpResponse,
        CancellationToken cancellationToken = default)
    {
        var errorResponse = await httpResponse.ReadAsErrorResponseAsync(cancellationToken);

        return BinanceSpotErrors.MapError(errorResponse!);
    }
}
