using RichillCapital.UseCases.Common;
using RichillCapital.UseCases.Instruments;

namespace RichillCapital.Contracts.Instruments;

public sealed record InstrumentResponse
{
    public required string Symbol { get; init; }

    public required string Description { get; init; }

    public required string Exchange { get; init; }
}

public static class InstrumentResponseMapping
{
    public static InstrumentResponse ToResponse(this InstrumentDto dto) =>
        new()
        {
            Symbol = dto.Symbol,
            Description = dto.Description,
            Exchange = dto.Exchange,
        };

    public static Paged<InstrumentResponse> ToPagedResponse(this PagedDto<InstrumentDto> dto) =>
        new()
        {
            Items = dto.Items.Select(ToResponse),
            Page = dto.Page,
            PageSize = dto.PageSize,
            TotalCount = dto.TotalCount,
        };
}