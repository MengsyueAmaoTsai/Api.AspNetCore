using System.Diagnostics;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance;

internal sealed class BinanceRestClient(
    ILogger<BinanceRestClient> _logger,
    HttpClient _httpClient,
    BinanceSignatureHandler _signatureHandler) :
    IBinanceRestClient
{
    private const string ApiKey = "guVqJIzZ29JZx2BTv9VbxxOr7IehQIIRRXABm53rawtThH0XcD8EeyzUtMbIaQ92";
    private const string SecretKey = "BPwSSG45zE8ABiZ6Zm4t9gJFJMo19ExjBqOQlmLcOM5LgfyYP6V5biYrsUkZfXxm";

    public async Task<Result<int>> PingAsync(CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();

        var response = await _httpClient.GetAsync("fapi/v1/ping", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.ReadAsErrorAsync(cancellationToken);
            _logger.LogWarning("{Error}", error);
            return Result<int>.Failure(error);
        }

        stopwatch.Stop();

        return Result<int>.With((int)stopwatch.ElapsedMilliseconds);
    }

    public async Task<Result<BinanceServerTimeResponse>> GetServerTimeAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("fapi/v1/time", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.ReadAsErrorAsync(cancellationToken);
            _logger.LogWarning("{Error}", error);
            return Result<BinanceServerTimeResponse>.Failure(error);
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var serverTime = JsonConvert.DeserializeObject<BinanceServerTimeResponse>(content);

        return Result<BinanceServerTimeResponse>.With(serverTime!);
    }

    public async Task<Result<BinanceExchangeInfoResponse>> GetExchangeInfoAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("fapi/v1/exchangeInfo", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.ReadAsErrorAsync(cancellationToken);
            _logger.LogWarning("{Error}", error);
            return Result<BinanceExchangeInfoResponse>.Failure(error);
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var exchangeInfo = JsonConvert.DeserializeObject<BinanceExchangeInfoResponse>(content);

        return Result<BinanceExchangeInfoResponse>.With(exchangeInfo!);
    }

    public async Task<Result> NewOrderAsync(
        string symbol,
        string side,
        string type,
        decimal quantity,
        string clientOrderId,
        CancellationToken cancellationToken = default)
    {
        var responseType = "RESULT";
        var positionSide = "BOTH";
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var queryString = $"symbol={symbol}&side={side}&type={type}&quantity={quantity}&newClientOrderId={clientOrderId}&newOrderRespType={responseType}&positionSide={positionSide}";
        queryString += $"&timestamp={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";

        var signature = _signatureHandler.Sign(SecretKey, queryString);

        var request = new HttpRequestMessage(HttpMethod.Post, "fapi/v1/order")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "symbol", symbol },
                { "side", side },
                { "type", type },
                { "quantity", quantity.ToString() },
                { "newClientOrderId", clientOrderId.ToString() },
                { "newOrderRespType", responseType },
                { "positionSide", positionSide },
                { "timestamp", timestamp.ToString() },
                { "signature", signature },
            })
        };

        request.Headers.Add("X-MBX-APIKEY", ApiKey);

        var response = await _httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.ReadAsErrorAsync(cancellationToken);
            _logger.LogWarning("{Error}", error);
            return Result.Failure(error);
        }

        return Result.Success;
    }
}
