using Newtonsoft.Json;

namespace RichillCapital.Max.Contracts;

public sealed record MaxCancelOrderResponse
{
    [JsonProperty("success")]
    public required bool Success { get; init; }
}