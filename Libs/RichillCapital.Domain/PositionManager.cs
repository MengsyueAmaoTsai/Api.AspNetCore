using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public interface IPositionManager
{
    Task<Result<Position>> GetOpenPositionAsync(
        AccountId accountId,
        Symbol symbol,
        CancellationToken cancellationToken = default);
}

internal sealed class PositionManager(
    IRepository<Position> _positionRepository) :
    IPositionManager
{
    public async Task<Result<Position>> GetOpenPositionAsync(
        AccountId accountId,
        Symbol symbol,
        CancellationToken cancellationToken = default)
    {
        var maybePosition = await _positionRepository
            .FirstOrDefaultAsync(
                p => p.AccountId == accountId && p.Symbol == symbol && p.Quantity > 0,
                cancellationToken);

        if (maybePosition.IsNull)
        {
            return Result<Position>.Failure(Error.NotFound($"Open position for account {accountId} and symbol {symbol} not found"));
        }

        var position = maybePosition.Value;
        return Result<Position>.With(position);
    }
}