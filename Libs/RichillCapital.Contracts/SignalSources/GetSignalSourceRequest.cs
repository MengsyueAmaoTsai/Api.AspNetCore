using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Contracts.SignalSources;

public sealed record GetSignalSourceRequest
{
    [FromRoute(Name = "signalSourceId")]
    public required string SignalSourceId { get; init; }
}