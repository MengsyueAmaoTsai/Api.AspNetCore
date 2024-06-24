using Microsoft.AspNetCore.Mvc;

using RichillCapital.UseCases.SignalSources.Get;

namespace RichillCapital.Contracts.SignalSources;

public sealed record GetSignalSourceRequest
{
    [FromRoute(Name = "signalSourceId")]
    public required string SourceId { get; init; }
}

public static class GetSignalSourceRequestMapping
{
    public static GetSignalSourceQuery ToQuery(this GetSignalSourceRequest request) =>
        new()
        {
            SourceId = request.SourceId,
        };
}