using RichillCapital.UseCases.Signals.Create;

namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalRequest
{
    // General Required Informations
    public required string SourceId { get; init; }
    public required DateTimeOffset CurrentTime { get; init; }
    public required string TradeType { get; init; }
    public required string Symbol { get; init; }
    public required decimal Price { get; init; }

    // TradingView Strategy Informations
    // market position size is always positive
    public required string MarketPosition { get; init; }
    public required decimal MarketPositionSize { get; init; }
    public required string PreviousMarketPosition { get; init; }
    public required decimal PreviousMarketPositionSize { get; init; }
}

public static class CreateSignalRequestMapping
{
    public static CreateSignalCommand ToCommand(this CreateSignalRequest request) =>
        new()
        {
            SourceId = request.SourceId,
            CurrentTime = request.CurrentTime,
        };
}