using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance.Spot;

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

        return await HandleResponseAsync(response);
    }

    public async Task<Result<ExchangeInfoResponse>> GetExchangeInfoAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            GeneralEndpoints.ExchangeInfo,
            cancellationToken);

        return await HandleResponseAsync<ExchangeInfoResponse>(response);
    }

    public async Task<Result<BinanceServerTimeResponse>> CheckServerTimeAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            GeneralEndpoints.CheckServerTime,
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

    private async Task<Result> HandleResponseAsync(HttpResponseMessage httpResponse)
    {
        try
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<BinanceErrorResponse>(responseContent);

                _logger.LogError(
                    "Failed to handle response: {statusCode} {errorContent}",
                    httpResponse.StatusCode,
                    errorResponse);

                return Result.Failure(Error.Unexpected("Failed to handle response"));
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

    private async Task<Result<TBinanceResponse>> HandleResponseAsync<TBinanceResponse>(HttpResponseMessage httpResponse)
    {
        try
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                var error = await ParseErrorResponseAsync(httpResponse);

                _logger.LogError(
                    "Failed to handle response: {statusCode} {errorContent}",
                    httpResponse.StatusCode,
                    error);

                return Result<TBinanceResponse>.Failure(error);
            }

            return Result<TBinanceResponse>.With(JsonConvert.DeserializeObject<TBinanceResponse>(responseContent)!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while handling response");

            return Result<TBinanceResponse>.Failure(Error.Unexpected("Failed to handle response"));
        }
    }

    private async Task<Error> ParseErrorResponseAsync(HttpResponseMessage httpResponse)
    {
        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        var errorResponse = JsonConvert.DeserializeObject<BinanceErrorResponse>(responseContent);

        return Error.Unexpected(
            errorResponse!.Code.ToString(),
            errorResponse!.Message);
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
