using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance;

internal sealed class BinanceSpotRestClient(
    ILogger<BinanceSpotRestClient> _logger,
    HttpClient _httpClient) :
    IBinanceSpotRestClient
{
    private const string ApiKey = "guVqJIzZ29JZx2BTv9VbxxOr7IehQIIRRXABm53rawtThH0XcD8EeyzUtMbIaQ92";
    private const string SecretKey = "BPwSSG45zE8ABiZ6Zm4t9gJFJMo19ExjBqOQlmLcOM5LgfyYP6V5biYrsUkZfXxm";

    private static class GeneralEndpoints
    {
        public const string TestConnectivity = "api/v3/ping";
        public const string ExchangeInfo = "api/v3/exchangeInfo";
        public const string CheckServerTime = "api/v3/time";
    }

    public async Task<Result> TestConnectivityAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            GeneralEndpoints.TestConnectivity,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

            _logger.LogError(
                "Failed to test connectivity: {statusCode} {errorContent}",
                response.StatusCode,
                errorContent);

            return Result.Failure(Error.Unexpected("Failed to test connectivity"));
        }

        return Result.Success;
    }

    public async Task<Result<ExchangeInfoResponse>> GetExchangeInfoAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            GeneralEndpoints.ExchangeInfo,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

            _logger.LogError(
                "Failed to get exchange info: {statusCode} {errorContent}",
                response.StatusCode,
                errorContent);

            return Result<ExchangeInfoResponse>.Failure(Error.Unexpected("Failed to get exchange info"));
        }

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var exchangeInfo = JsonConvert.DeserializeObject<ExchangeInfoResponse>(responseContent);

        _logger.LogInformation("Exchange info: {exchangeInfo}", exchangeInfo);
        return Result<ExchangeInfoResponse>.With(exchangeInfo!);
    }

    public async Task<Result<DateTimeOffset>> CheckServerTimeAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            GeneralEndpoints.CheckServerTime,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

            _logger.LogError(
                "Failed to check server time: {statusCode} {errorContent}",
                response.StatusCode,
                errorContent);

            return Result<DateTimeOffset>.Failure(Error.Unexpected("Failed to check server time"));
        }

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var serverTime = JsonConvert.DeserializeObject<BinanceServerTimeResponse>(responseContent);

        var serverTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(serverTime!.ServerTime);

        return Result<DateTimeOffset>.With(serverTimeOffset);
    }

    public async Task<Result> NewOrderAsync(
        string symbol = "LTCBTC",
        string side = "BUY",
        string type = "LIMIT",
        string timeInForce = "GTC",
        decimal quantity = 1,
        decimal price = 0.1m,
        long recvWindow = 5000,
        long timestamp = 1499827319559,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            Symbol = symbol,
            Side = side,
            Type = type,
            TimeInForce = timeInForce,
            Quantity = quantity,
            Price = price,
            RecvWindow = recvWindow,
            Timestamp = timestamp,
        };

        _logger.LogInformation("Submitting new order: {request}", request);
        var queryString = $"symbol={symbol}&side={side}&type={type}&timeInForce={timeInForce}&quantity={quantity}&price={price}&recvWindow={recvWindow}&timestamp={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
        var signature = GenerateSignature(queryString);

        queryString += $"&signature={signature}";

        _logger.LogInformation("Query string: {queryString}", queryString);

        _httpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", ApiKey);

        var response = await _httpClient.PostAsync(
            "api/v3/order",
            new StringContent(queryString, Encoding.UTF8, "application/x-www-form-urlencoded"),
            cancellationToken);

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = JsonConvert.DeserializeObject<BinanceErrorResponse>(responseContent);

            _logger.LogError(
                "Failed to submit new order: {statusCode} {errorContent}",
                response.StatusCode,
                errorResponse);

            return Result.Failure(Error.Unexpected("Failed to submit new order"));
        }

        _logger.LogInformation("New order submitted successfully. {content}", responseContent);

        return Result.Success;
    }

    private static string GenerateSignature(string queryString)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey));

        return BitConverter
            .ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(queryString)))
            .Replace("-", string.Empty)
            .ToLower();
    }
}
