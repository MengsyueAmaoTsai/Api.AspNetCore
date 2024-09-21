using Newtonsoft.Json;

namespace RichillCapital.Max.Contracts;

internal sealed record MaxErrorResponse
{
    [JsonProperty("success")]
    public required bool Success { get; init; }

    [JsonProperty("error")]
    public required MaxInnerErrorResponse Error { get; init; }
}

internal sealed record MaxInnerErrorResponse
{
    [JsonProperty("code")]
    public required int Code { get; init; }

    [JsonProperty("message")]
    public required string Message { get; init; }
}