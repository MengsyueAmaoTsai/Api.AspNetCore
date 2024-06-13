using RichillCapital.UseCases.Common;
using RichillCapital.UseCases.Signals;

namespace RichillCapital.Contracts.Signals;

public sealed record SignalResponse
{
    public required string Id { get; init; }
}

public static class SignalExtensions
{
    public static SignalResponse ToResponse(this SignalDto signal) =>
        new()
        {
            Id = signal.Id,
        };

    public static Paged<SignalResponse> ToPagedResponse(this PagedDto<SignalDto> paged) =>
        new()
        {
            Items = paged.Items.Select(signal => signal.ToResponse()),
        };
}