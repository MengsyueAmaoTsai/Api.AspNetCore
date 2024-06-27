using Microsoft.AspNetCore.Mvc;

using RichillCapital.UseCases.Signals.List;

namespace RichillCapital.Contracts;

public sealed record ListRequest
{
    [FromQuery(Name = "searchTerm")]
    public string? SearchTerm { get; init; }

    [FromQuery(Name = "sortBy")]
    public string? SortBy { get; init; }

    [FromQuery(Name = "order")]
    public string? Order { get; init; }

    [FromQuery(Name = "page")]
    public required int Page { get; init; }

    [FromQuery(Name = "pageSize")]
    public required int PageSize { get; init; }
}

public static class ListRequestMapping
{
    public static ListSignalsQuery ToQuery(this ListRequest request) =>
        new()
        {
            SearchTerm = request.SearchTerm ?? string.Empty,
            SortBy = request.SortBy ?? string.Empty,
            Order = request.Order ?? string.Empty,
            Page = request.Page,
            PageSize = request.PageSize,
        };
}