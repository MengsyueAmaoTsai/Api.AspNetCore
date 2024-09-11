using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Contracts.Instruments;

public sealed record GetInstrumentRequest
{
    [FromRoute(Name = "symbol")]
    public required string Symbol { get; init; }
}