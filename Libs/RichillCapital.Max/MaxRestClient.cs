using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RichillCapital.Http;
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
        var response = await _httpClient.GetAsync("api/v3/timestamp", cancellationToken);
        return await HandleResponse<MaxServerTimeResponse>(response);
    }

    public async Task<Result<MaxMarketResponse[]>> ListMarketsAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("api/v3/markets", cancellationToken);
        return await HandleResponse<MaxMarketResponse[]>(response);
    }

    public async Task<Result<MaxUserInfoResponse>> GetUserInfoAsync(CancellationToken cancellationToken = default)
    {
        var path = "api/v3/info";
        var nonce = DateTimeOffset.UtcNow.Millisecond;

        var bodyToEncode = new
        {
            Nonce = nonce,
            Path = path,
        };

        var encodedBody = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bodyToEncode)));
        var signature = _signatureHandler.Sign(SecretKey, path, encodedBody);

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, path);
        httpRequest.Headers.Add("X-MAX-ACCESSKEY", ApiKey);
        httpRequest.Headers.Add("X-MAX-PAYLOAD", encodedBody);
        httpRequest.Headers.Add("X-MAX-SIGNATURE", signature);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
        return await HandleResponse<MaxUserInfoResponse>(response);
    }

    public async Task<Result> SubmitOrderAsync(CancellationToken cancellationToken = default)
    {
        var path = "api/v3/wallet/{pathWalletType}/order";
        var nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var body = new
        {
            Nonce = nonce,
            Path = path,
        };

        var payloadBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body)));

        var signature = _signatureHandler.Sign(SecretKey, path, payloadBase64);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, path);

        httpRequest.Headers.Add("X-MAX-ACCESSKEY", ApiKey);
        httpRequest.Headers.Add("X-MAX-PAYLOAD", payloadBase64);
        httpRequest.Headers.Add("X-MAX-SIGNATURE", signature);

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
