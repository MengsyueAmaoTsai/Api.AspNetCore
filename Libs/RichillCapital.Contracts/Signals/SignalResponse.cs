using RichillCapital.UseCases.Common;
using RichillCapital.UseCases.Signals;

namespace RichillCapital.Contracts.Signals;

public sealed record SignalResponse
{
    public required string Id { get; init; }

    public required string SourceId { get; init; }

    public DateTimeOffset Time { get; init; }

    public required int Latency { get; init; }
}

public static class SignalExtensions
{
    public static SignalResponse ToResponse(this SignalDto signal) =>
        new()
        {
            Id = signal.Id,
            SourceId = signal.SourceId,
            Time = signal.Time,
            Latency = signal.Latency,
        };

    public static Paged<SignalResponse> ToPagedResponse(this PagedDto<SignalDto> paged) =>
        new()
        {
            Items = paged.Items.Select(signal => signal.ToResponse()),
        };
}