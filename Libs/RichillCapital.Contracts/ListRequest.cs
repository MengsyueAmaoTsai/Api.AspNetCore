using Microsoft.AspNetCore.Mvc;

using RichillCapital.UseCases.Signals.List;

namespace RichillCapital.Contracts;

public sealed record ListRequest
{
    [FromQuery(Name = "sortBy")]
    public string? SortBy { get; init; }

    [FromQuery(Name = "order")]
    public string? Order { get; init; }
}

public static class ListRequestMapping
{
    public static ListSignalsQuery ToQuery(this ListRequest request) =>
        new()
        {
            SortBy = request.SortBy ?? string.Empty,
            Order = request.Order ?? string.Empty,
        };
}