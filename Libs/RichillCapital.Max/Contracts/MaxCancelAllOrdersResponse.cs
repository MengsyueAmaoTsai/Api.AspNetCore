using Newtonsoft.Json;

namespace RichillCapital.Max.Contracts;

public sealed record MaxCancelAllOrdersResponse
{
    [JsonProperty("error")]
    public required string ErrorMessage { get; init; }

    [JsonProperty("order")]
    public required MaxOrderResponse Order { get; init; }
}